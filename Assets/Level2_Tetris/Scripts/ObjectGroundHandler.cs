using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGroundHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isLocked = false;

    void Start()
    {
        // Get the Rigidbody2D component attached to this object
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collides with something tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Make the Rigidbody static to stop all movement
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isLocked = true;
        }
    }
}
