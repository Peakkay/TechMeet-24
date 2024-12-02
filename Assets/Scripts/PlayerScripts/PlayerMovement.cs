using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float tileSize = 1f;   // Size of each tile (1 unit)

    public Vector3 targetPosition;
    private bool isMoving = false; // Check if the player is moving
    public bool canMove = true;   // Flag to control movement

    private void Start()
    {
        // Start at the current position
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (canMove)
        {
            Move(); // Only allow movement if player can move
        }
    }

    public void Move(Vector3 direction)
    {
        // Only allow movement if not currently moving
        if (!isMoving)
        {
            // Calculate the new target position
            Vector3 newPosition = targetPosition + direction * tileSize;

            // Check if the new position is clear
            if (IsPathClear(newPosition))
            {
                targetPosition = newPosition; // Set the new target position
                isMoving = true; // Start moving
            }
        }
        else
        {
            // If already moving and the key is still held down, do not change target
            return; // Don't do anything if already moving
        }
    }

    private bool IsPathClear(Vector3 newPosition)
    {
        // Check if the new position overlaps with any colliders
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(newPosition, tileSize / 2); // Check entire tile area
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Obstacle"))
            {
                return false; // Path is blocked if any obstacle is in the tile
            }
        }
        return true; // Path is clear
    }

    private void Move()
    {
        // Move smoothly to the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Snap to target position when close enough
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition; // Snap to the target position
            isMoving = false; // Stop moving

            // Check if the key is still pressed for continuous movement
            HandleContinuousMovement();
        }
    }

    private void HandleContinuousMovement()
    {
        // Check if movement keys are still pressed and set new target position
        if (Input.GetKey(KeyCode.W) && IsPathClear(targetPosition + Vector3.up * tileSize))
        {
            targetPosition += Vector3.up * tileSize;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A) && IsPathClear(targetPosition + Vector3.left * tileSize))
        {
            targetPosition += Vector3.left * tileSize;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S) && IsPathClear(targetPosition + Vector3.down * tileSize))
        {
            targetPosition += Vector3.down * tileSize;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D) && IsPathClear(targetPosition + Vector3.right * tileSize))
        {
            targetPosition += Vector3.right * tileSize;
            isMoving = true;
        }
    }

    // Public property to expose the isMoving variable
    public bool IsMoving
    {
        get { return isMoving; }
    }
}
