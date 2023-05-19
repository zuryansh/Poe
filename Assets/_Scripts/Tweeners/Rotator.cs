using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotator : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Vector3 rotateTo;
    [SerializeField] Ease easeType;
    [SerializeField] LoopType loopType = LoopType.Restart;

    void Start()
    {
        transform.DORotate(rotateTo, time , RotateMode.FastBeyond360).SetLoops(-1,loopType).SetEase(easeType);
    }

   void OnDestroy() 
    {
        transform.DOKill();
    }
}
