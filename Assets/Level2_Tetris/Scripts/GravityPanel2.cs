using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPanel2 : MonoBehaviour
{
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            if (other.CompareTag("Heavy"))
            {
                rb.gravityScale = 5;
            }
            else
            {
                rb.gravityScale = 1;
            }
        }
    }
}
