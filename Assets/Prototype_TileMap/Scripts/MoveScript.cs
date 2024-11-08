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
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        float moveInput2 = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveInput * moveSpeed, moveInput2 * moveSpeed);



    }

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

}
