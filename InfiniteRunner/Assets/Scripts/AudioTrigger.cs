using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] AudioClip audioToPlay;
    [SerializeField] AudioPlayer audioPlayerPref;

    [Header("Settings")]
    [SerializeField] string triggerTag = "Player";
    [SerializeField] float volume = 1;
    [SerializeField] float pitch = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == triggerTag)
        {
            PlayAudio();
        }
    }

    private void PlayAudio()
    {
        AudioPlayer newPlayer = Instantiate(audioPlayerPref);
        newPlayer.PlayAudio(audioToPlay, volume, pitch);
    }
}
