using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    [SerializeField] string objectTag;
    [SerializeField] UnityEvent OnTriggerEnter;
    [SerializeField] UnityEvent OnTriggerExit;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag(objectTag))
        {
            OnTriggerEnter?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.transform.CompareTag(objectTag))
        {
            OnTriggerExit?.Invoke();
        }
    }
}
