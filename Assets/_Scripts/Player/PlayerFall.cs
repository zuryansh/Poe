using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : State
{
    public PlayerFall(Player player): base(player){}

    

    public override void Start(State previous)
    {

        //Fall gravity
        player.RB.gravityScale = player.GravityScale * player.FallGravityMultiplier;
        player.canMove = true;

    }

    public override void Update()
    {
        if(player.IsGrounded)
        {
            player.SetState(player.runState);
        }
        if(player.InputHandler.JumpInput && player.JumpsLeft>0) // wall jump
        {
            player.SetState(player.jumpingState);
            return;
        }
        if(player.CanWallSlide && player.IsFalling)
        {
            player.SetState(player.wallSlidingState);
            return;
        }
        if(player.InputHandler.DashInput && player.CanDash)
        {
            player.SetState(player.dashState);
            return;
        }

    }

    public override void FixedUpdate()
    {
        player.Move();// Airborne Movement
        ClampFallSpeed();
    }




    void ClampFallSpeed()
    {
        if (player.RB.velocity.y < player.MaxFallVelocity) //max fall speed has to be negative
        {
            player.SetYVelocity(player.MaxFallVelocity);
        }
    }
    
    public override void Disable()
    {
        //remove fall gravity
        player.RB.gravityScale = player.GravityScale;
        player.SetAnimationState(player.LandAnim);

    }
}
