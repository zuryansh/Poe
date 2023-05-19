using UnityEngine;
using DG.Tweening;

public class ClickShake : MonoBehaviour
{
    [SerializeField] Transform applyTo;
    [SerializeField] float duration;

    public void Shake()
    {
        applyTo.DOShakeScale(duration ,0.3f);
    }
}
