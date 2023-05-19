using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : Projectile
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int damage;

    void Start()
    {
        Destroy(gameObject, 5f);
        rb.velocity = velocity;
    }





    public override void OnHit(Transform hit)
    {
        I_TakeDamage damageable = hit.GetComponent<I_TakeDamage>();
        if(damageable != null) damageable.TakeDamage(damage , gameObject);
        
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D other) 
    {
        OnHit(other.transform);
        
    }



}
