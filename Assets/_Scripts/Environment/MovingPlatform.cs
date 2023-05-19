using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
    [SerializeField] float duration;
    [SerializeField] float timeOffset;


    public void Init(Vector2 start , Vector2 end , float time)
    {
        startPos = start;
        endPos = end;
        duration = time;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeOffset);
        transform.DOMove(endPos, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }


    [ContextMenu("Set Start Pos")]
    void SetStartPos()
    {
        startPos = transform.position;
    }
    [ContextMenu("Set End Pos")]
    void SetEndPos()
    {
        endPos = transform.position;
    }
}
