using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DestroyEffectType{ ScaleOut , SpinOut};

public class DestroyEffects : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Ease ease;
    [SerializeField] DestroyEffectType type;


    public void Disappear(GameObject user )
    {
        if(type == DestroyEffectType.ScaleOut) ScaleOut();
        else if(type == DestroyEffectType.SpinOut) SpinOut();
        Destroy(user, time);            
    }

    void SpinOut()
    {
        transform.DOScale(Vector3.zero, time).SetEase(ease);
        transform.DORotate(new Vector3(0, 0, 360f), time, RotateMode.FastBeyond360).SetEase(Ease.Linear);
    }

    void ScaleOut()
    {
        transform.DOScale(Vector3.zero, time).SetEase(ease);
    }

   void OnDestroy() 
    {
        transform.DOKill();
    }
}
