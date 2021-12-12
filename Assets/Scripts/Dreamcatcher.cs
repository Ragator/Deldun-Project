using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dreamcatcher : Interactable
{
    [SerializeField] private AudioClip interactSound;

    private AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    override protected void Interact()
    {
        myAudioSource.PlayOneShot(interactSound, 0.8f);
    }
}
