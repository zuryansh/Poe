using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;
    [SerializeField] AudioSource musicSource, effectsSource;
    [SerializeField] AudioClip testClip;
    [SerializeField] bool canPlaySound = true;

    void Awake()
    {
        if(SoundManager.inst != null && SoundManager.inst != this) Destroy(gameObject); // destorys itself if a reference already exists
        else inst = this;

        DontDestroyOnLoad(gameObject);

    }

    public void PlaySound(AudioClip clip)
    {
        if(canPlaySound) effectsSource.PlayOneShot(clip);
    }

    [ContextMenu("Test Sound")]
    void TestSound()
    {
        SoundManager.inst.PlaySound(testClip);
    }

    public void ToggleSound()
    {
        canPlaySound = !canPlaySound;

        musicSource.mute = !canPlaySound;
        effectsSource.mute = !canPlaySound;
    }

}
