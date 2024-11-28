using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundGameOver : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        // Play the initial sound
        SoundManager.Instance.PlaySoundGameOver(_clip);
    }
}
