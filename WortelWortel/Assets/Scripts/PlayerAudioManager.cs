using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioClip[] gunShot;
    public AudioClip[] grunt;
    AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void PlayRandomClip(AudioClip[] ClipToPlay)
    {

        AudioClip randomClip = ClipToPlay[Mathf.RoundToInt(Random.Range(0, ClipToPlay.Length))];

        audioSource.PlayOneShot(randomClip);

    }
}
