using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Vector2 velocity;

    public virtual void OnHit(Transform hit){}
}
