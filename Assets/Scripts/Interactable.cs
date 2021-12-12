using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrompt;

    private bool canInteract = false;

    private void Update()
    {
        if (canInteract)
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
        if (collision.gameObject.CompareTag(DeldunProject.Tags.player))
        {
            ShowButtonPrompt();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.player))
        {
            HideButtonPrompt();
            canInteract = false;
        }
    }

    private void ShowButtonPrompt()
    {
        buttonPrompt.SetActive(true);
    }

    private void HideButtonPrompt()
    {
        buttonPrompt.SetActive(false);
    }
}