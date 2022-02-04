using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dreamcatcher : Interactable
{
    [SerializeField] private AudioClip interactSound;

    private AudioSource myAudioSource;

    protected override void Start()
    {
        base.Start();
        myAudioSource = GetComponent<AudioSource>();
    }

    override protected void Interact()
    {
        myAudioSource.PlayOneShot(interactSound, 0.5f);

        // Stop player movement
        // Reset enemies
        // Save progress
        // Open checkpoint menu
    }
}
