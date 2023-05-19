using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    public void Play()
    {
        SoundManager.inst.PlaySound(clip);
    }
}
