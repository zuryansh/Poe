using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TeleporterType{ None, Entry , Exit , SpawnPortal}

public class Portal : MonoBehaviour , I_Interactable
{
    [SerializeField] Portal exit;
    [SerializeField] Transform obj;
    [SerializeField] float teleportDelay;
    public static event Action<TeleporterType> OnTeleport;
    public bool isSpawnPoint;
    public bool isExitPortal;
    bool isTeleporting;

    Bobber bobber;

    void Start()
    {
        bobber = GetComponentInChildren<Bobber>();
    }


    public IEnumerator EnterPortal(Transform obj)
    {
        if(isTeleporting) yield break;

        isTeleporting = true;
        OnTeleport?.Invoke(TeleporterType.Entry);
        GetComponentInChildren<Bobber>().enabled = false;

        yield return new WaitForSeconds(teleportDelay);

        TeleportObject(obj);
    }

    public void TeleportObject(Transform obj ) 
    {
        if (isExitPortal)
        {
            isTeleporting = false;
            GameManager.inst.SetState(GameStates.PlayerWon);
            return;
        }

        if(exit == null && !isExitPortal) exit = this;

        if (obj!=null)
        {
            obj.transform.position = exit.transform.position;
            ExitPortal();
        }
    }

    void ExitPortal()
    {
        GetComponentInChildren<Bobber>().enabled = true;
        OnTeleport?.Invoke(TeleporterType.Exit);

        isTeleporting = false;
    }

    public void Interact(PlayerInteractor interactor)
    {
        StartCoroutine(EnterPortal(interactor.transform));
    }

}
