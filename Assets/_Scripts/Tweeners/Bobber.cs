using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bobber : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Vector3 offset;
    [SerializeField] Ease ease;
    Quaternion startRot;
    Vector2 startPos;
    // Start is called before the first frame update




    // void Start()
    // {

    //     StartTweening();
    // }

    void Start()
    {
        // Debug.Log(transform.localPosition + offset , gameObject);
        transform.DOLocalMove((transform.localPosition + offset), time).SetLoops(-1, LoopType.Yoyo).SetEase(ease).SetId(1);
    }

   void OnDestroy() 
    {
        transform.DOKill();
    }

}
