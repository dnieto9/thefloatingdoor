using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public Sprite jumpingRightSprite;
    public Sprite jumpingLeftSprite;

    public AudioSource jumpSound;
    public AudioSource groundTouchSound;
    public AudioSource movingSound; // New moving sound

    private Vector3 originalScale;
    private float lastDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        if (jumpSound == null)
        {
            jumpSound = gameObject.AddComponent<AudioSource>();
        }

        if (groundTouchSound == null)
        {
            groundTouchSound = gameObject.AddComponent<AudioSource>();
        }

        if (movingSound == null)
        {
            movingSound = gameObject.AddComponent<AudioSource>();
        }

        movingSound.loop = true; // Ensure the moving sound loops
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            lastDirection = moveInput;

            if (isGrounded)
            {
                // Play or restart the moving sound
                if (!movingSound.isPlaying)
                {
                    movingSound.Play();
                }
                else if ((moveInput > 0 && lastDirection < 0) || (moveInput < 0 && lastDirection > 0))
                {
                    movingSound.Stop(); // Restart the sound when changing direction
                    movingSound.Play();
                }
            }
        }
        else
        {
            // Stop the moving sound if the player is grounded but not moving
            if (isGrounded && movingSound.isPlaying)
            {
                movingSound.Stop();
            }
        }

        if (isGrounded)
        {
            animator.enabled = true;
            animator.SetFloat("Speed", Mathf.Abs(moveInput));

            if (moveInput > 0)
            {
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            else if (moveInput < 0)
            {
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
        else
        {
            animator.enabled = false;
            if (rb.velocity.x > 0)
            {
                spriteRenderer.sprite = jumpingRightSprite;
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            else if (rb.velocity.x < 0)
            {
                spriteRenderer.sprite = jumpingLeftSprite;
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGrounded)
        {
            if (groundTouchSound != null)
            {
                groundTouchSound.Play();
            }
        }

        isGrounded = true;
        animator.enabled = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

        // Stop the moving sound when leaving the ground
        if (movingSound.isPlaying)
        {
            movingSound.Stop();
        }
    }
}
