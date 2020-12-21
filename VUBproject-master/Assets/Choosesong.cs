using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choosesong : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    // Start is called before the first frame update
    void Start()
    {
       audioSource.clip = audioClip[Random.Range(0, audioClip.Length)];
       audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
