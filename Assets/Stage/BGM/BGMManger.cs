using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManger : MonoBehaviour
{
    [SerializeField]
    private AudioClip bgm;
    private AudioSource _audioSource;
    private AudioSource audioSource
    {
        get
        {
            if (_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
            return _audioSource;
        }
    }

    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }
}
