using UnityEngine;
using System;

public class LookAtMe : MonoBehaviour
{
    public static Action<Transform> OnStart;

    void Start() => OnStart?.Invoke(transform);
 
}
