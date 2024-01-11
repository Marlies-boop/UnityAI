using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int damageAmount = 10;
    public float totalMoveDistance = 5f;
    public float directionChangeCooldown = 2f; // Adjust cooldown time as needed

    private bool movingRight = true;
    private Vector3 startPosition;
    private float directionChangeTimer;

    void Start()
    {
        startPosition = transform.position;
        directionChangeTimer = directionChangeCooldown;
    }

    void Update()
    {
        // Move the enemy left or right
        Move();

        // Check for player collision
        CheckPlayerCollision();
    }

    void Move()
    {
        // Move the enemy
        if (movingRight)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Change direction when reaching the edge of the moveDistance
        if (Mathf.Abs(transform.position.x - startPosition.x) >= totalMoveDistance / 2)
        {
            // Check if the cooldown timer has elapsed
            if (directionChangeTimer <= 0f)
            {
                movingRight = !movingRight;
                directionChangeTimer = directionChangeCooldown; // Reset the cooldown timer

                // Flip the x scale when changing direction
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        // Update the cooldown timer
        directionChangeTimer -= Time.deltaTime;
    }

    void CheckPlayerCollision()
    {
        // Check if the enemy collides with the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Hurt the player
                Debug.Log("Hit player");
                /*PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }*/
            }
        }
    }
}
