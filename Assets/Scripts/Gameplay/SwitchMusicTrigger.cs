using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip musicToSwitchTo;
    [SerializeField] private float musicVolume = 0.5f;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindWithTag(DeldunProject.Tags.audioManager).GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(DeldunProject.Tags.player) && musicToSwitchTo != null)
        {
            audioManager.SwitchMusic(musicToSwitchTo, musicVolume);
        }
    }
}
