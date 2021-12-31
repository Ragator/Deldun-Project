using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Item myitem;
    [SerializeField] private AudioClip openingSound;

    private SpriteRenderer mySpriteRenderer;
    private Inventory myInventory;
    private AudioSource myAudioSource;

    protected override void Start()
    {
        base.Start();
        myInventory = GameObject.FindWithTag(DeldunProject.Tags.inventory).GetComponent<Inventory>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    protected override void Interact()
    {
        // Open the chest
        mySpriteRenderer.sprite = openedSprite;
        myAudioSource.PlayOneShot(openingSound, 0.4f);
        canInteract = false;
        HideButtonPrompt();

        // Add the item to inventory
        myInventory.AddItem(myitem);
    }
}