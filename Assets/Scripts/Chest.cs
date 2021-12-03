using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool canInteract = false;
    private bool canOpen = true;
    private SpriteRenderer mySpriteRenderer;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private GameObject buttonPrompt;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Open();
            }
        }
    }

    public void Open()
    {
        if (canOpen)
        {
            canOpen = false;
            mySpriteRenderer.sprite = openedSprite;
        }
        else
        {
            canOpen = true;
            mySpriteRenderer.sprite = closedSprite;
        }
    }

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

    public void ShowButtonPrompt()
    {
        buttonPrompt.SetActive(true);
    }

    public void HideButtonPrompt()
    {
        buttonPrompt.SetActive(false);
    }
}
