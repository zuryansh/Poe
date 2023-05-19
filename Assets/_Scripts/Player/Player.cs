using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerSkills
{
    Dash, DoubleJump, WallJump
}

public class Player : StateMachine
{

    #region Variables

    [SerializeField] PlayerData data;

    public Animator anim { get; private set; }
    public readonly int IdleAnim = Animator.StringToHash("Idle");
    public readonly int JumpAnim = Animator.StringToHash("Jump");
    public readonly int LandAnim = Animator.StringToHash("Landing");
    public readonly int HitAnim = Animator.StringToHash("Hit");
    public readonly int DashAnim = Animator.StringToHash("Dash");
    public readonly int TeleportAnim = Animator.StringToHash("Teleport");
    public readonly int TeleportOutAnim = Animator.StringToHash("TeleportOut");


    [SerializeField] string showState;

    //differnt states = Run , jumping , falling , wallSliding, Dash , Hit
    public State runState { get; private set; }
    public State jumpingState { get; private set; }
    public State fallingState { get; private set; }
    public State wallSlidingState { get; private set; }
    public State dashState { get; private set; }
    public State hitState{ get; private set; }

    public ParticleSystem impactParticles;
    public ParticleSystem dashParticles;
    public ParticleSystem DeathParticles;

    public AudioClip JumpSound;
    public AudioClip HitSound;

    Rigidbody2D rb;



    [Header("Jump")]
    public int JumpsLeft;


    [Header("Dash")]
    public int DashesLeft;

    [Header("Checks")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Vector3 lastGroundedPos;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isfalling;
    [SerializeField] bool canWallSlide;
    [SerializeField] bool canDash;
    public bool IsInvincible;
    public bool canMove;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool isGoingUp;

    // [Header("Animation")]
    public int currentAnimationState;
    public float LockedTill{get;private set; }

    //On Player Hit
    public GameObject HitSource;
    [SerializeField] float invincibilityTime;

    float _gravityScale;
    float _inputx;

    #endregion

    #region Properties
    public PlayerData Data => data;
    public Rigidbody2D RB => rb;
    public float MoveSpeed => data.moveSpeed;

    public float MaxFallVelocity => data.maxFallVelocity;

    public int NoOfJumps => data.noOfJumps;
    public float JumpForce => data.jumpForce;
    public float FallGravityMultiplier => data.fallGravityMultiplier;
    public float JumpCutMultiplier => data.jumpCutMultiplier;

    public float WallSlideMultiplier => data.wallSlideMultiplier;
    public float WallJumpForce => data.wallJumpForce;
    public float HorizontalJumpForce => data.horizontalJumpForce;


    public float DashForce => data.dashForce;
    public float DashTime => data.dashTime;
    public float DashCooldown => data.dashCooldown;
    public int NoOfDashes => data.noOfDashes;

    public Transform GroundCheck => groundCheck;
    public Vector2 LastGroundedPos => lastGroundedPos;
    public bool IsGrounded => isGrounded;
    public bool IsFacingRight => isFacingRight;
    public bool CanWallSlide => canWallSlide;
    public bool CanDash => canDash;
    public bool IsFalling => isfalling;
    public bool IsGoingUp => isGoingUp;
    public bool CanDoubleJump => (JumpsLeft > 0 && data.skills.Contains(PlayerSkills.DoubleJump));

    public LayerMask GroundLayer => data.groundLayer;
    public Vector2 GroundCheckSize => data.groundCheckSize;
    public float GravityScale => _gravityScale;

    public float InvincibilityTime => invincibilityTime;

    public PlayerInputHandler InputHandler{ get; private set; }
    public float InputX => _inputx;



    #endregion

    #region MonoBehaviour Methods

    void Awake()
    {
        if(data==null) Debug.LogError("Player Data was not assigned in inspector");
        data.SetReceiver(this);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _gravityScale = rb.gravityScale;
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    void OnEnable()
    {
        Portal.OnTeleport += TeleportEffects;

    }

    // Start is called before the first frame update
    void Start()
    {


        runState = new PlayerRun(this);
        jumpingState = new PlayerJump(this);
        fallingState = new PlayerFall(this);
        wallSlidingState = new PlayerWallSlide(this);
        dashState = new PlayerDash(this);
        hitState = new PlayerHit(this);



        SetState(runState);
    }

    private void Update()
    {
        //checks
        isGrounded = CheckIfGrounded();
        isfalling = CheckIfFalling();
        canWallSlide = CheckForWallSlide();
        isGoingUp = CheckIfGoingUp();
        canDash = CheckIfCanDash();
        if(isGrounded) lastGroundedPos = transform.position;

        
        //Input
        // _inputx = Input.GetAxisRaw("Horizontal");
        _inputx = InputHandler.MovementInput.x;

        //Update
        showState = state.ToString();
        state.Update();

        //Animation
        
        //flip cgaracter
        if (_inputx < 0 && isFacingRight)
        {
            Flip();
        }
        else if (_inputx > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    void FixedUpdate() => state.FixedUpdate();

    void OnDisable() 
    {
        Portal.OnTeleport -= TeleportEffects;
    }

    #endregion

    #region Checks 

    bool CheckIfGrounded() => Physics2D.OverlapBox(groundCheck.position, GroundCheckSize, 0, GroundLayer);

    bool CheckIfFalling() => (rb.velocity.y < -2f);

    bool CheckForWallSlide() => (Physics2D.Raycast(wallCheck.position, transform.right, 0.1f, GroundLayer) && data.skills.Contains(PlayerSkills.WallJump));

    bool CheckIfGoingUp() => (rb.velocity.y > 0.3f);

    bool CheckIfCanDash() => (DashesLeft > 0 && data.skills.Contains(PlayerSkills.Dash));


    #endregion

    #region  Setters
    
    public void SetXVelocity(float val) =>  rb.velocity = new Vector2(val, rb.velocity.y);

    public void SetYVelocity(float val) => rb.velocity = new Vector2(rb.velocity.x, val);

    public void SetDash(bool allowed) => canDash = allowed;


    #endregion

    #region  Animation
    
    int LockState(int s , float t)
    {
        LockedTill = Time.time + t;
        return s;
    }

    public void SetAnimationState(int state)
    {
        if(Time.time<LockedTill) return;
        if(state == currentAnimationState) return;

        
        currentAnimationState = state;

        if (state == DashAnim) currentAnimationState = LockState(DashAnim, data.DashAnimDuration);
        else if (state == LandAnim) currentAnimationState = LockState(LandAnim, data.LandAnimDuration); 
        else if (state == TeleportAnim) currentAnimationState = LockState(TeleportAnim, Data.TeleportAnimDuration);
        else if (state == TeleportOutAnim) currentAnimationState = LockState(TeleportOutAnim, Data.TeleportAnimDuration);

        else { currentAnimationState = state; }

        anim.CrossFade(currentAnimationState, 0, 0);

    }

    #endregion

    public void Move()
    {
        if(!canMove) return;
        // get the maxSpeed
        float targetSpeed = InputX * MoveSpeed;

        // get the difference b/w current and max speed
        float speedDif = targetSpeed - RB.velocity.x;
        RB.AddForce(speedDif * Vector2.right, ForceMode2D.Impulse); // impulse feels more snappy but FORCE feels more floaty
    }


    public void Flip()
    {
        if(!canMove) return;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }


    public void TeleportEffects(TeleporterType type)
    {
        IEnumerator EnableMovement(float time) { yield return new WaitForSeconds(time); canMove = true; }

        if (type == TeleporterType.Entry)
        {
            // anim.CrossFade(TeleportAnim, 0, 0);
            canMove = false;
            rb.velocity = Vector2.zero;
            SetAnimationState(TeleportAnim);
        }
        else if (type == TeleporterType.Exit)
        {
            canMove = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(EnableMovement(data.TeleportAnimDuration));
            SetAnimationState(TeleportOutAnim);
        }

    }

    public void Die()
    {
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    

}
