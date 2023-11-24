using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;

    [Header("UI SFX")]
    public AudioClip uiClickedSound;

    void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
        audioSource.playOnAwake = false;
    }


    public void PlayUICilckSound()
    {
        audioSource.clip = uiClickedSound;
        if (!audioSource.isPlaying) { audioSource.Play(); }
    }
}
