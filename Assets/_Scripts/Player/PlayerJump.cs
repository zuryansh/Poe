using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : State
{
    public PlayerJump(Player player) : base(player){}

    
    bool canJumpCancel = true;

    public override void Start(State previous)
    {
        SoundManager.inst.PlaySound(player.JumpSound);

        player.SetAnimationState(player.JumpAnim);
        player.SetYVelocity(0);

        if(previous == player.fallingState || previous== player.jumpingState)
        {
                        
            DoubleJump();
        }

        else if(previous == player.wallSlidingState)
        {
            canJumpCancel = false;
            player.StartCoroutine(CO_WallJump());
        }
        else
        {

            Jump(player.JumpForce);
        }
        
    }

    public override void Update()
    {
        //Input
        if(player.InputHandler.JumpCutInput)
        {
            JumpCut();
        }
        if(player.InputHandler.JumpInput && player.JumpsLeft>0) // double jump
        {
            player.SetState(player.jumpingState);
            return;
        }

        #region State Changes

        if(player.InputHandler.DashInput && player.CanDash)
        {
            player.SetState(player.dashState);
            return;
        }
        
        if(player.IsFalling)
        {
            player.SetState(player.fallingState);
            return;
        }    
        #endregion
    }

    public override void FixedUpdate()
    {

        player.Move();// Airborne Movement
        
    }

    public void Jump(float jumpForce)
    {

        player.impactParticles.Play();
        player.RB.AddForce(jumpForce * Vector2.up,ForceMode2D.Impulse);
    }
    
    void DoubleJump()
    {
        if(!player.CanDoubleJump) return;

        if(!player.IsGrounded) player.JumpsLeft--;
        canJumpCancel = true;
        player.canMove = true;
        Jump(player.JumpForce);
    }

    IEnumerator CO_WallJump()
    {
        
        player.canMove = false;

        Vector2 upForce = player.WallJumpForce * Vector3.up;
        Vector2 horizontalForce = player.HorizontalJumpForce * (-player.transform.right);

        player.RB.AddForce(upForce , ForceMode2D.Impulse);
        player.RB.AddForce(horizontalForce , ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);

        player.canMove = true;
    }

    void JumpCut()
    {
        // makes it so that the player can control the jumo height
        if (canJumpCancel)
        {
            if (player.RB.velocity.y > 0)
            {
                //reduce y velocity 
                player.RB.AddForce(Vector2.down * player.RB.velocity.y * (1 - player.JumpCutMultiplier), ForceMode2D.Impulse);
            }
        }
    }
}
