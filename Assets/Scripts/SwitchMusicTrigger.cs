using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour
{
    [SerializeField] AudioClip musicToSwitchTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && musicToSwitchTo != null)
        {
            AudioManager.instance.SwitchMusic(musicToSwitchTo);
        }
    }
}
