using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHit : State
{

    public PlayerHit(Player player):base(player){}


    // Start is called before the first frame update
    public override void Start(State previous)
    {
        SoundManager.inst.PlaySound(player.HitSound);
        player.anim.CrossFade(player.HitAnim, 0, 1); //anim
        player.Die();
        
        GameManager.inst.SetState(GameStates.PlayerDied);

        // if(player.HitSource.CompareTag("DeadlyHazard")) player.StartCoroutine(CO_ReturnToLastGround()); 
        // else player.SetState(player.runState);
    }


    // public IEnumerator CO_ReturnToLastGround()
    // {
    //     Vector2 offset = new Vector2(0, 1);

    //     player.canMove = false;
    //     player.RB.isKinematic = true;
    //     player.transform.DOMove(player.LastGroundedPos+offset, 1);


    //     yield return new WaitForSeconds(1f);
    //     player.canMove = true;
    //     player.RB.isKinematic = false;
    //     player.SetState(player.runState);
    // }



}
