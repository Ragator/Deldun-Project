using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator myAnimator;

    //[SerializeField] private Slider healthBar;
    //[SerializeField] private float maxHealth = 10f;
    //private float currentHealth;

    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;

    private Rigidbody2D myRigidBody;
    //private BoxCollider2D boxCollider;
    private Vector2 moveDelta;

    private void Start()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
        //myRigidBody.velocity += moveDelta.normalized * moveSpeed * Time.deltaTime;

        myRigidBody.AddForce(moveDelta.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);

/*        if (moveDelta != Vector2.zero)
        {
            myRigidBody.MovePosition(myRigidBody.position + moveDelta * moveSpeed * Time.fixedDeltaTime);
        }*/
    }

    public void TakeDamage(float damage)
    {
        GameManager.instance.PlayerTakeDamage(damage);

        audioSource.PlayOneShot(hitSound, 0.5f);

        // Activate Invincibility Frames
    }
}