using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform swordSprite;
    [SerializeField] private int weaponDamage = 20;
    [SerializeField] private float weaponKnockbackStrength = 500f;
    [SerializeField] private float knockbackToPlayerStrength = 500f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float slashSpeed = 1500f;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private Image radialSprite;
    [SerializeField] private AudioClip slashSound;
    [SerializeField] private int attackStaminaCost = 15;
    [SerializeField] private DamageType attackType;

    private PlayerStats myPlayerStats;
    private Player myPlayer;
    private Transform playerTransform;
    private Transform playerSpriteTransform;

    private AudioSource myAudioSource;
    private float rotation;
    private Vector3 mouseWorldPosition;
    public bool canRotate = true;
    private CapsuleCollider2D damageCollider;
    private float attackAngle;

    [Header("Scaling")]
    public WeaponScaling brawnScaling;
    public WeaponScaling skillScaling;
    public WeaponScaling visionScaling;

    private void Start()
    {
        damageCollider = GetComponent<CapsuleCollider2D>();
        myAudioSource = GetComponent<AudioSource>();
        myPlayerStats = GameObject.FindWithTag(DeldunProject.Tags.player).GetComponent<PlayerStats>();
        myPlayer = myPlayerStats.gameObject.GetComponent<Player>();
        playerTransform = myPlayerStats.gameObject.transform;
        playerSpriteTransform = myPlayer.playerSprite.transform;
    }

    private void Update()
    {
        // Get mouse position
        Vector3 mousePos = Input.mousePosition;

        // Convert mouse position to screen space
        mousePos.z = Camera.main.nearClipPlane;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        // Rotate towards mouse position
        Vector3 difference = mouseWorldPosition - transform.position;
        rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotation - 90);

        
        if (transform.localRotation.eulerAngles.z > 180)
        {
            playerSpriteTransform.localScale = Vector2.one;
            swordSprite.localScale = Vector2.one;
        }
        else if (transform.localRotation.eulerAngles.z < 180)
        {
            playerSpriteTransform.localScale = new Vector2(-1, 1);
            swordSprite.localScale = new Vector2(-1, 1);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && canRotate)
        {
            if (myPlayerStats.GetStamina() >= attackStaminaCost)
            {
                canRotate = false;
                radialSprite.enabled = true;
                myPlayerStats.StopStaminaRegeneration();
                myPlayer.speedModifier = 0.5f;
                myPlayer.isChargingWeapon = true;
            }
        }

        if (radialSprite.enabled)
        {
            FillSlashRadialSlider();
        }

        if (Input.GetMouseButtonUp(0) && !canRotate)
        {
            radialSprite.enabled = false;
            myPlayer.speedModifier = 0;
            StartCoroutine(Slash(rotation));
        }
    }

    private void FixedUpdate()
    {
        if (canRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotation - 90), rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void FillSlashRadialSlider()
    {
        Vector2 swordDirection = (transform.position + transform.up * 10) - transform.position;
        Vector2 mouseDirection = mouseWorldPosition - transform.position;
        attackAngle = Vector2.SignedAngle(swordDirection, mouseDirection) / 360f;

        radialSprite.fillClockwise = attackAngle < 0;
        radialSprite.fillAmount = Mathf.Abs(attackAngle);
    }

    private IEnumerator Slash(float slashRotation)
    {
        if (Mathf.Abs(attackAngle) > 0.03f)
        {
            damageCollider.enabled = true;
            myAudioSource.PlayOneShot(slashSound, 0.05f);
            float timeout = 0;
            myPlayerStats.ReduceStamina(attackStaminaCost);

            while (Mathf.Abs(Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 0, slashRotation - 90))) > 1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, slashRotation - 90), slashSpeed * Time.fixedDeltaTime);

                timeout += Time.fixedDeltaTime;

                if (timeout > 0.5f)
                {
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        canRotate = true;
        myPlayerStats.StartStaminaRegeneration();
        myPlayer.speedModifier = 1f;
        myPlayer.isChargingWeapon = false;
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.enemyHitbox))
        {
            EnemyStats enemy = (EnemyStats)collision.GetComponent<CharacterHitbox>().myCharacterStats;

            enemy.TakeDamage(weaponDamage, attackType);

            enemy.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * weaponKnockbackStrength);

            playerTransform.gameObject.GetComponent<Rigidbody2D>().AddForce((playerTransform.position - collision.transform.position).normalized * knockbackToPlayerStrength);

            myAudioSource.PlayOneShot(hitSound, 0.4f);
        }
    }
}

public enum WeaponScaling
{
    S = 30,
    A = 25,
    B = 20,
    C = 15,
    D = 10,
    E = 5,
    F = 0
}