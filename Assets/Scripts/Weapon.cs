using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform swordSprite;
    [SerializeField] private float weaponDamage = 2f;
    [SerializeField] private float weaponKnockbackStrength = 500f;
    [SerializeField] private float knockbackToPlayerStrength = 500f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float slashSpeed = 1500f;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private Image radialSprite;
    [SerializeField] private AudioClip slashSound;

    private AudioSource myAudioSource;
    private float rotation;
    private Vector3 mouseWorldPosition;
    public bool canRotate = true;
    private CapsuleCollider2D damageCollider;

    private void Start()
    {
        damageCollider = GetComponent<CapsuleCollider2D>();
        myAudioSource = GetComponent<AudioSource>();
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
            playerSprite.localScale = Vector2.one;
            swordSprite.localScale = Vector2.one;
        }
        else if (transform.localRotation.eulerAngles.z < 180)
        {
            playerSprite.localScale = new Vector2(-1, 1);
            swordSprite.localScale = new Vector2(-1, 1);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && canRotate)
        {
            canRotate = false;
            
            // TO DO
            radialSprite.enabled = true;
        }

        if (radialSprite.enabled)
        {
            FillSlashRadialSlider();
        }

        if (Input.GetMouseButtonUp(0) && !canRotate)
        {
            StartCoroutine(Slash(rotation));
            radialSprite.enabled = false;
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
        float angle = Vector2.SignedAngle(swordDirection, mouseDirection) / 360f;

        radialSprite.fillClockwise = angle < 0;
        radialSprite.fillAmount = Mathf.Abs(angle);
    }

    private IEnumerator Slash(float slashRotation)
    {
        damageCollider.enabled = true;
        myAudioSource.PlayOneShot(slashSound, 0.05f);
        float timeout = 0;

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

        canRotate = true;

        damageCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.enemy))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * weaponKnockbackStrength);

            player.gameObject.GetComponent<Rigidbody2D>().AddForce((player.position - collision.transform.position).normalized * knockbackToPlayerStrength);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(collision.transform.position), rotationSpeed);

            myAudioSource.PlayOneShot(hitSound, 0.4f);
        }
    }
}
