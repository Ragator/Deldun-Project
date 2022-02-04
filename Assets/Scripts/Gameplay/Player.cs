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
    public GameObject playerSprite;
    [SerializeField] private PlayerStats myPlayerStats;

    [SerializeField] private float iFramesDuration = 1.5f;
    [SerializeField] private float iFramesDeltaTime = .15f;

    public bool isInvincible = false;

    [SerializeField] private int dashStaminaCost = 10;
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float dashCooldown = 0.2f;

    public bool isChargingWeapon = false;

    [SerializeField] private AudioClip hitSound;
    //private AudioSource audioSource;
    private GameManager gameManager;
    private Keybinds myKeybinds;

    public SpriteRenderer helmetSlot;
    public SpriteRenderer chestpieceSlot;
    public Transform weaponSlot;

    private Rigidbody2D myRigidBody;
    private Vector2 moveDelta;

    public float speedModifier = 1f;

    #region Singleton Pattern
    public static Player instance;

    private void Awake()
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
    }
    #endregion

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //audioSource = GetComponent<AudioSource>();

        gameManager = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<GameManager>();
        myKeybinds = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<Keybinds>();

        gameManager.myPlayer = this;
    }

    private void Update()
    {
        // Movement input
        if (gameManager.isPlayerInputEnabled)
        {
            moveDelta.x = Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.right])) - Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.left]));
            moveDelta.y = Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.up])) - Convert.ToInt32(Input.GetKey(myKeybinds.keybinds[Action.down]));
        }

        // Dash
        if (Input.GetKeyDown(myKeybinds.keybinds[Action.dash]) && moveDelta != Vector2.zero && !isChargingWeapon)
        {
            Dash();
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
        if (gameManager.isPlayerInputEnabled)
        {
            myRigidBody.AddForce(moveDelta.normalized * moveSpeed * speedModifier * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void Dash()
    {
        if (myPlayerStats.GetStamina() >= dashStaminaCost)
        {
            myRigidBody.AddForce(moveDelta.normalized * dashSpeed, ForceMode2D.Impulse);
            myPlayerStats.ReduceStamina(dashStaminaCost);
            myPlayerStats.StartStaminaRegeneration();
            BecomeInvincible();
        }
    }

    public void BecomeInvincible()
    {
        StartCoroutine(ActivateIFrames());
    }

    private IEnumerator ActivateIFrames()
    {
        isInvincible = true;

        for (float i = 0; i < iFramesDuration; i += iFramesDeltaTime)
        {
            playerSprite.SetActive(!playerSprite.activeSelf);
            yield return new WaitForSeconds(iFramesDeltaTime);
        }

        playerSprite.SetActive(true);
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