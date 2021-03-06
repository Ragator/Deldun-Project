using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Item keyToUnlock;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private AudioClip closedSound;
    [SerializeField] private AudioClip openingSound;
    [SerializeField] private BoxCollider2D stopCollider;

    private Inventory myInventory;
    private SpriteRenderer mySprite;
    private AudioSource myAudioSource;

    protected override void Start()
    {
        base.Start();
        myInventory = GameObject.FindWithTag(DeldunProject.Tags.inventory).GetComponent<Inventory>();
        mySprite = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    protected override void Interact()
    {
        if (myInventory.CheckKey(keyToUnlock))
        {
            mySprite.sprite = openSprite;
            canInteract = false;
            HideButtonPrompt();
            stopCollider.enabled = false;
            myAudioSource.PlayOneShot(openingSound, 0.3f);
        }
        else
        {
            myAudioSource.PlayOneShot(closedSound, 0.3f);
        }
    }
}
