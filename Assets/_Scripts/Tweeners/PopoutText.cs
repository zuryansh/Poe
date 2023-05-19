using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopoutText : MonoBehaviour
{
    [SerializeField] float promptTime;
    [SerializeField] Color diasbledColor;
    TextMeshProUGUI text;
    [SerializeField] Color enabledColor;

    Vector3 startPos;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        enabledColor = text.color;
        startPos = transform.localPosition;
        Hide();
        
    }

    public void Show(string txt)
    {
        // enabled = true;
        text.text = txt;
        text.color = enabledColor;
        transform.DOLocalMove(startPos + new Vector3(0,0.2f,0), promptTime).SetEase(Ease.InOutSine);
        
    }

    public void Hide() => StartCoroutine(CO_Hide());

    IEnumerator CO_Hide()
    {
        transform.DOLocalMove( startPos , promptTime-0.2f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(promptTime - 0.2f); 
        text.color = diasbledColor;
        yield break;
    }
}
