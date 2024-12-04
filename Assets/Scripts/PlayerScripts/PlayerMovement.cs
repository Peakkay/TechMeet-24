using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float tileSize = 1f;   // Size of each tile (1 unit)
    public Animator animator;

    public Vector3 targetPosition;
    private bool isMoving = false; // Check if the player is moving
    public bool canMove = true;   // Flag to control movement

    private Vector3 lastMoveDirection = Vector3.down; // Store the last movement direction
    private Vector3 currentInput = Vector3.zero; // Current input direction

    private void Start()
    {
        // Start at the current position
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (canMove)
        {
            ProcessInput();
            UpdateAnimator(); // Update animator parameters
            Move(); // Handle the movement logic
        }
    }

    private void ProcessInput()
    {
        // Reset current input
        currentInput = Vector3.zero;

        // Capture input even if the player doesn't move
        if (Input.GetKey(KeyCode.W))
        {
            currentInput = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentInput = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentInput = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentInput = Vector3.right;
        }

        // Update lastMoveDirection if there's valid input
        if (currentInput != Vector3.zero)
        {
            lastMoveDirection = currentInput;
            if (!isMoving && IsPathClear(targetPosition + currentInput * tileSize))
            {
                Move(currentInput);
            }
        }
    }

    private void UpdateAnimator()
    {
        // Determine movement direction
        Vector3 movement = targetPosition - transform.position;

        // Normalize movement direction and update animator
        Vector3 normalizedMovement = movement.normalized;
        animator.SetFloat("Horizontal", normalizedMovement.x);
        animator.SetFloat("Vertical", normalizedMovement.y);

        // Update speed based on whether the player is moving
        float speed = movement.magnitude > 0.01f ? moveSpeed : 0f;
        animator.SetFloat("Speed", speed);

        // Use last move direction for idle blend tree
        if (speed < 0.01f)
        {
            animator.SetFloat("Horizontal", lastMoveDirection.x);
            animator.SetFloat("Vertical", lastMoveDirection.y);
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
        }
    }

    // Public property to expose the isMoving variable
    public bool IsMoving
    {
        get { return isMoving; }
    }
}
