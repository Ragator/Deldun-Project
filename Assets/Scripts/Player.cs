using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private SpriteRenderer playerSprite;

    [SerializeField] private float iFramesDuration = 1.5f;
    [SerializeField] private float iFramesDeltaTime = .15f;

    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private GameManager gameManager;
    private Keybinds myKeybinds;

    private Rigidbody2D myRigidBody;
    private Vector2 moveDelta;

    private bool isInvincible = false;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        gameManager = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<GameManager>();
        myKeybinds = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<Keybinds>();
    }

    private void Update()
    {
        // Movement input
        if (gameManager.isInputEnabled)
        {
            moveDelta.x = Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.right])) - Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.left]));
            moveDelta.y = Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.up])) - Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.down]));
        }

        // Walking animation
        if (moveDelta.x != 0 || moveDelta.y != 0)
        {
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.isInputEnabled)
        {
            myRigidBody.AddForce(moveDelta.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        gameManager.PlayerTakeDamage(damage);

        audioSource.PlayOneShot(hitSound, 0.5f);

        // Activate Invincibility Frames
        StartCoroutine(ActivateIFrames());
    }

    private IEnumerator ActivateIFrames()
    {
        isInvincible = true;

        for (float i = 0; i < iFramesDuration; i += iFramesDeltaTime)
        {
            playerSprite.enabled = !playerSprite.enabled;
            yield return new WaitForSeconds(iFramesDeltaTime);
        }

        playerSprite.enabled = true;
        isInvincible = false;
    }

    public void GainCurrency(int amount)
    {
        gameManager.GainCurrency(amount);
    }

    public void LoseCurrency(int amount)
    {
        gameManager.LoseCurrency(amount);
    }
}