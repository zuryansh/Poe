using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float fallTime;
    [SerializeField] float resetTime;
    [SerializeField] Collider2D trig;
    [SerializeField] Collider2D col;
    [SerializeField] Color disabledColor;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] GameObject particles;

    Color _activeColor;

    void Start()
    {
        _activeColor = rend.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) Invoke("Fall", fallTime);
    }

    void Fall()
    {
        rend.color = disabledColor;
        col.enabled = false;
        trig.enabled = false;
        particles.SetActive(false);
        Invoke("Reset", resetTime);
    }

    void Reset()
    {
        rend.color = _activeColor;
        col.enabled = true;
        trig.enabled = true;
        particles.SetActive(true);
    }
}
