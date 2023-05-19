using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{

    [SerializeField] float resetTime;
    [SerializeField] Collider2D col;
    [SerializeField] Color disabledColor;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] float timeOffset;

    Color _activeColor;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeOffset);
        _activeColor = rend.color;
        Invoke("Fall", resetTime);
    }



    void Fall()
    {
        rend.color = disabledColor;
        col.enabled = false;
        Invoke("Reset", resetTime);
    }

    void Reset()
    {
        rend.color = _activeColor;
        col.enabled = true;
        Invoke("Fall", resetTime);
    }
}
