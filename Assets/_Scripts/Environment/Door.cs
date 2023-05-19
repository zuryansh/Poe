using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Vector2 closePos;
    [SerializeField] float time;
    public bool isOpened{ get;private set; }


    void Start() {
        isOpened = false;
        closePos = transform.position;
    }

    [ContextMenu("Open")]
    public void Open()
    {
        if(isOpened) return;
        transform.DOMove(transform.position + offset, time).SetEase(Ease.InOutSine);
        isOpened = true;
    }

    [ContextMenu("Close")]

    public void Close()
    {
        if(!isOpened) return;

        transform.DOMove(closePos, time).SetEase(Ease.InOutSine);
        isOpened = false;

    }

    // [ContextMenu("Set Start Pos")]
    // public void SetClosePos() => closePos = transform.position;
    // [ContextMenu("Set End Pos")]
    // public void SetOpenPos() => openPos = transform.position;

}
