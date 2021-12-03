using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private Rigidbody2D myRigidBody;
    private Vector2 moveDelta;

    private bool isInvincible = false;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        gameManager = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<GameManager>();
    }

    private void Update()
    {
        // Movement input
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");

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
        myRigidBody.AddForce(moveDelta.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void TakeDamage(float damage)
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
}