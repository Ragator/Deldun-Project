using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float nextWaypointDistance = 0.5f;
    [SerializeField] private float seekingDistance = 5f;

    [SerializeField] private float knockbackStrength = 400f;
    [SerializeField] private float selfKnockbackStrength = 400f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float damage = 2f;

    [SerializeField] private GameObject canvas;

    private float currentHealth;

    private Path path;
    private int currentWaypoint = 0;
#pragma warning disable IDE0052 // Remove unread private members
    private bool reachedEndOfPath = false;
#pragma warning restore IDE0052 // Remove unread private members

    private Seeker seeker;
    private GameObject player;
    //private Rigidbody2D playerRigidbody;
    private Rigidbody2D myRigidbody;


    private void Start()
    {
        // Pathfinding
        myRigidbody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        player = GameObject.FindWithTag(DeldunProject.Tags.player);
        //playerRigidbody = player.GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f);

        // Stats
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        UpdateHealthBar(currentHealth);
    }

    private void UpdatePath()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < seekingDistance)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(myRigidbody.position, player.transform.position, OnPathComplete);
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - myRigidbody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        myRigidbody.AddForce(force);

        float distance = Vector2.Distance(myRigidbody.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Swap sprite direction
        if (myRigidbody.velocity.x <= 0.01f)
        {
            transform.localScale = new Vector2(-1f, 1f);
            canvas.transform.localScale = new Vector2(-1f, 1f);
        }
        else if (myRigidbody.velocity.x >= -0.01f)
        {
            transform.localScale = new Vector2(1f, 1f);
            canvas.transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
/*        if (collision.gameObject.CompareTag("Sword"))
        {
            // Knockback
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            myRigidbody.AddForce(knockbackDirection * knockbackStrength);
        }*/

        if (collision.gameObject.CompareTag(DeldunProject.Tags.player))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);

            // Knockback the player
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * knockbackStrength);

            // Knockback away from the player
            myRigidbody.AddForce((transform.position - collision.transform.position).normalized * selfKnockbackStrength);
        }
    }

    public void TakeDamage(float damage)
    {
        canvas.SetActive(true);
        currentHealth -= damage;

        //Debug.Log("The enemy took " + damage + " damage!");
        //Debug.Log("Its health is now at " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        UpdateHealthBar(currentHealth);
        // Knockback rigidbody
        // Knockback player rigidbody
        // Decrement health
        // Check health, die if <= 0
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateHealthBar(float value)
    {
        healthBar.value = value;
    }
}
