using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] int damage;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            // OnSpikeCollision?.Invoke(damage , gameObject);
            I_TakeDamage damageable = other.transform.GetComponent<I_TakeDamage>();
            if(damageable != null) damageable.TakeDamage(damage , gameObject);
        }
    }
}
