using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPanel : MonoBehaviour
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

    public void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.gravityScale = 1;
        }
    }
}
