using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform swordSprite;

    [SerializeField] private float weaponDamage = 2f;
    [SerializeField] private float weaponKnockbackStrength = 500f;
    [SerializeField] private float knockbackToPlayerStrength = 500f;
    [SerializeField] private float rotationSpeed = 300f;

    private float rotation;
    private Vector3 mouseWorldPosition;

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
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotation - 90), rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * weaponKnockbackStrength);

            player.gameObject.GetComponent<Rigidbody2D>().AddForce((player.position - collision.transform.position).normalized * knockbackToPlayerStrength);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(collision.transform.position), rotationSpeed);
        }
    }
}
