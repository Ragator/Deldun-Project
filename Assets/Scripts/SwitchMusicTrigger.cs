using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip musicToSwitchTo;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && musicToSwitchTo != null)
        {
            audioManager.SwitchMusic(musicToSwitchTo);
        }
    }
}
