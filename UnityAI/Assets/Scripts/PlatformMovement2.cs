using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float targetPositionX = 10f; // Adjust the target position as needed
    public float targetPositionLeft;
    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the platform
        MovePlatform();
    }

    void MovePlatform()
    {
        //print(movingRight);
        // Calculate movement direction
        float direction = movingRight ? 1f : -1f;

        // Move the platform
        Vector2 movement = new Vector2(direction, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        // Check if the platform has reached the target position
        if ((direction > 0 && transform.position.x >= targetPositionX) ||
            (direction < 0 && transform.position.x <= targetPositionLeft))
        {
            // Change direction and reset velocity
            movingRight = !movingRight;
            //Debug.Log(movingRight);
            rb.velocity = Vector2.zero;
        }
    }
}
