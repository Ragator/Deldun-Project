using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Interactable : MonoBehaviour
{
    private TextMeshProUGUI buttonPrompt;
    private Keybinds myKeybinds;

    private bool playerInRange = false;
    protected bool canInteract = true;

    protected virtual void Start()
    {
        buttonPrompt = GameObject.FindWithTag(DeldunProject.Tags.buttonPrompt).GetComponent<TextMeshProUGUI>();
        myKeybinds = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<Keybinds>();
    }

    private void Update()
    {
        if (playerInRange && canInteract)
        {
            if (Input.GetKeyDown(myKeybinds.keybinds[Action.interact]))
            {
                Interact();
            }
        }
    }

    protected abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.player) && canInteract && collision.isTrigger)
        {
            ShowButtonPrompt();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.player) && collision.isTrigger)
        {
            HideButtonPrompt();
            playerInRange = false;
        }
    }

    protected void ShowButtonPrompt()
    {
        buttonPrompt.enabled = true;
    }

    protected void HideButtonPrompt()
    {
        buttonPrompt.enabled = false;
    }
}