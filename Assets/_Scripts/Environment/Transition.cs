using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] float transitionDuration;
    Animator anim;
    float delay;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SlideIn()
    {
        anim.SetTrigger("In");
    }
    public void SlideOut()
    {
        
        anim.SetTrigger("Out");
    }



    // [SerializeField] Vector3 inPos;
    // [SerializeField] Vector3 outPos;
    // [SerializeField] float duration;
    // [SerializeField] float delay;

    // Vector3 startPos;


    // // Start is called before the first frame update
    // void Start()
    // {
    //     startPos = transform.position;

    //     yield return new WaitForSeconds(delay);
    //     SlideIn();
    // }

    // public IEnumerator SlideIn()
    // {
    //     transform.DOMove(inPos, duration).SetEase(Ease.InOutSine);
    // }

    // public void SlideOut()
    // {

    //     transform.DOMove(outPos, duration).SetEase(Ease.InOutSine);

    // }

    // [ContextMenu("inPos")]
    // void SetInPos() => inPos = transform.position;
    // [ContextMenu("outPos")]
    // void SetOutPos() => outPos = transform.position;
}
