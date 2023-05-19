using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : State
{
    public PlayerRun(Player player) : base(player) {  }

    public override void Start(State previousState)
    {
        player.SetAnimationState(player.IdleAnim);

        if (player.IsGrounded)
        {
            player.JumpsLeft = player.NoOfJumps;
            player.DashesLeft = player.NoOfDashes;
        }
        player.canMove = true;
    }

    public override void Update()
    {
        #region State Changes

        if(player.InputHandler.JumpInput)
        {
            player.SetState(player.jumpingState);
            return;

        }
        if(player.IsFalling)
        {
            player.SetState(player.fallingState);
            return;

        }
        if(player.InputHandler.DashInput && player.CanDash)
        {
            player.SetState(player.dashState);
            return;
        }        
        #endregion
        if(player.currentAnimationState != player.IdleAnim) player.SetAnimationState(player.IdleAnim);

    }

    public override void FixedUpdate()
    {
         player.Move();// Movement
    }
}
