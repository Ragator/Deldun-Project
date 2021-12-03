using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private float volume;
    [SerializeField] private ParticleSystem clouds;
    [SerializeField] private float cloudsDuration = 0.1f;

    private void Awake()
    {
        clouds.Stop();
    }

    private void Step()
    {
        myAudioSource.PlayOneShot(footstepSound, volume);

        StartCoroutine(ActivateClouds());
    }

    private IEnumerator ActivateClouds()
    {
        clouds.Play();
        yield return new WaitForSeconds(cloudsDuration);
        clouds.Stop();
    } 
}
