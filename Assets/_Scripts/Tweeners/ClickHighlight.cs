using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ClickHighlight : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] float highlighDuration;
    [SerializeField] Ease highlightEase;
    [SerializeField] Transform applyTo;
    [SerializeField] AudioClip clip;

    Vector3 ogScale;

    void Start()
    {
        ogScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.inst.PlaySound(clip);
        applyTo.DOScale(transform.localScale + (Vector3.one*0.1f) , highlighDuration).SetEase(highlightEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        applyTo.DOScale(ogScale , highlighDuration).SetEase(highlightEase);
    }




}
