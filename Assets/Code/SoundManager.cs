using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _gameover, _spawn, _backsound;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _spawn.PlayOneShot(clip);
    }

    public void PlaySoundGameOver(AudioClip clip)
    {
        _gameover.PlayOneShot(clip);
    }

    // Fungsi untuk memutar backsound
    public void PlayBacksound(AudioClip clip)
    {
        if (_backsound.isPlaying)
        {
            _backsound.Stop();
        }

        _backsound.clip = clip;
        _backsound.Play();
    }

    // Fungsi untuk menghentikan backsound
    public void StopBacksound()
    {
        _backsound.Stop();
    }
}
