using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public static AudioManager instance;

    private AudioSource BGM;

/*    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }*/

    private void Start()
    {
        BGM = GetComponent<AudioSource>();
    }

    public void SwitchMusic(AudioClip newMusic)
    {
        if (BGM.clip.name != newMusic.name)
        {
            BGM.Stop();
            BGM.clip = newMusic;
            BGM.Play();
        }
    }
}
