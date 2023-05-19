using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : State
{
    public PlayerWallSlide(Player player): base(player){}

    public override void Start(State previous)
    {
        // player.anim.CrossFade(player.WallSlideAnim , 0, 0);

        player.JumpsLeft = player.NoOfJumps;
        player.DashesLeft = player.NoOfDashes;

        player.SetYVelocity(0);
        player.RB.gravityScale = player.GravityScale * player.WallSlideMultiplier;

    }

    public override void Update()
    {

        player.SetXVelocity(0);

        #region  State Changes

        if(player.InputHandler.DashInput && player.CanDash)
        {
            player.SetState(player.dashState);
            return;
        }
        if(player.InputHandler.JumpInput ) // wall jump
        {
            player.SetState(player.jumpingState);
            return;
        }
        if(player.IsGrounded)
        {
            player.SetState(player.runState);
            return;
        }

        else if(!player.CanWallSlide)
        {
            player.SetState(player.fallingState);
            return;
        }
        #endregion
    
        
    }

    public override void Disable()
    {
        player.RB.gravityScale = player.GravityScale;
    }
}
