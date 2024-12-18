using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    public float speed;
    private Vector2 movement;
    public Transform targetPosition;

    public LayerMask UnwalkableLayer;
    public LayerMask MoveableLayer;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    //private bool isGrounded = false;
    private Rigidbody2D rb;

    Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Debug.Log("MoveScript is being used");
    }
    // Update is called once per frame
    void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        float moveInput2 = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveInput * moveSpeed, moveInput2 * moveSpeed);

        animator.SetFloat("XMovement", moveInput);
        animator.SetFloat("YMovement", moveInput2);
        //Debug.Log(movement);

    }

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        
        //animator.SetFloat("XMovement", movement.x);
        //Debug.Log("xVel: " + animator.GetFloat("xVelocity"));
        //animator.SetFloat("YMovement", movement.y);
    }

}
