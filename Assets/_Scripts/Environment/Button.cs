using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{

    [SerializeField] UnityEvent OnButtonDown;
    [SerializeField] UnityEvent OnButtonUp;
    [SerializeField] Color pressedColor;
    [SerializeField] Color normalColor;

    SpriteRenderer ren;

    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        normalColor = ren.color;
    }

    public void Interact()
    {
        ren.color = pressedColor;
        OnButtonDown?.Invoke();
    }

    void StopInteraction()
    {
        ren.color = normalColor;
        OnButtonUp?.Invoke();
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            Interact();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            StopInteraction();
        }
    }
}
