using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDash : State
{
    public PlayerDash(Player player) : base(player){}

    public override void Start(State previous)
    {
        player.SetAnimationState(player.DashAnim);

        if(previous == player.wallSlidingState)
        {
        player.StartCoroutine(CO_Dash(-player.transform.right)); // dash away from wall

        }
        else
        {
            player.StartCoroutine(CO_Dash(player.transform.right)); // dash toward direction
        }
    }

    IEnumerator CO_Dash(Vector2 direction)
    {
        player.DashesLeft--;
        player.SetDash(false);
        player.dashParticles.Play();
        player.RB.velocity = Vector2.zero;
        player.RB.gravityScale = 0;
        player.RB.AddForce(player.DashForce * direction, ForceMode2D.Impulse);

        yield return new WaitForSeconds(player.DashTime); // apply force for dash time
        player.SetState(player.runState); //defaukt state        
        player.StartCoroutine(CO_ResetDash()); // reset the dash after cooldown is over

    }

    public override void Disable()
    {
        player.RB.gravityScale = player.GravityScale;
    }

    IEnumerator CO_ResetDash()
    {
        yield return new WaitForSeconds(player.DashCooldown);

        player.SetDash(true);

    }
}
