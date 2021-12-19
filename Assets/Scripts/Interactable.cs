using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrompt;

    private bool playerInRange = false;
    protected bool canInteract = true;

    private void Update()
    {
        if (playerInRange && canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    protected abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.player) && canInteract)
        {
            ShowButtonPrompt();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.player))
        {
            HideButtonPrompt();
            playerInRange = false;
        }
    }

    protected void ShowButtonPrompt()
    {
        buttonPrompt.SetActive(true);
    }

    protected void HideButtonPrompt()
    {
        buttonPrompt.SetActive(false);
    }
}