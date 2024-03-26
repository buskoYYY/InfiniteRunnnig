using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] AudioSource audioSrs;

    public void PlayAudio(AudioClip audioToPlay, float volume, float pitch, bool isDestroyOnFinish =true)
    {
        audioSrs.clip = audioToPlay;
        audioSrs.volume = volume;
        audioSrs.pitch = pitch;
        audioSrs.Play();

        if (isDestroyOnFinish )
        {
            Invoke("DestroySelf", audioToPlay.length);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

