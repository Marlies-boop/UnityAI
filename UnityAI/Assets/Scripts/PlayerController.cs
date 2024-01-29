using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;    // Speed of player movement
    public float jumpForce = 10f;   // Force applied when jumping
    public int maxHealth = 3;       // Maximum health of the player
    private int currentHealth;      // Current health of the player
    private Rigidbody2D rb;
    private bool isGrounded;

    private bool canTakeDamage = true;
    public float damageCooldown = -10f;
    public float timeBeforeDeath = 1f;

    public float fallDeathY = 5f; // Set the Y-coordinate below which the player dies

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Check if the player is below the fallDeathY coordinate
        if (transform.position.y < fallDeathY)
        {
            Die();
        }

        // Check if player is grounded
        isGrounded = IsGrounded();

        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    bool IsGrounded()
    {
        float raycastLength = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    // Function to handle player taking damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(1);
            Debug.Log(currentHealth);
            StartCoroutine(DamageCooldown());
        }
    }
    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
    void Die()
    {
        // Add any death-related logic here
        Debug.Log("Player died!");
        Respawn();
    }

    void Respawn()
    {
        // You can add additional respawn logic here if needed
        currentHealth = maxHealth; // Reset health
        // Move player back to a respawn point or initial position
        transform.position = new Vector2(0f, 0f); // Change this to your desired respawn position
    }
}
