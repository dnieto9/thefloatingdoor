using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab3 : MonoBehaviour
{
    private Rigidbody2D heldObject;
    private Vector3 mouseOffset;
    public float rotationSpeed = 100f;
    public float reverseGravityScale = -1f; // Gravity scale for "falling up"

    void Start()
    {
        foreach (Rigidbody2D rb in FindObjectsOfType<Rigidbody2D>())
        {
            if (rb.CompareTag("Ground")) continue;
            ObjectGroundHandler lockScript = rb.GetComponent<ObjectGroundHandler>();
            if (lockScript != null && lockScript.isLocked) continue;

            rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                             RigidbodyConstraints2D.FreezePositionY |
                             RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.attachedRigidbody != null)
            {
                if (hit.collider.CompareTag("UI")) return;
                if (hit.collider.CompareTag("Player")) return;
                if (hit.collider.CompareTag("Ground")) return;

                ObjectGroundHandler lockScript = hit.collider.GetComponent<ObjectGroundHandler>();
                if (lockScript != null && lockScript.isLocked) return;

                heldObject = hit.collider.attachedRigidbody;
                mouseOffset = heldObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Stop rotation
                heldObject.angularVelocity = 0f; // Reset angular velocity
                heldObject.constraints = RigidbodyConstraints2D.None;
                heldObject.isKinematic = true;
            }
        }

        if (heldObject != null)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset;
            newPosition.z = 0;
            heldObject.position = newPosition;

            if (Input.GetKey(KeyCode.A))
            {
                heldObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                heldObject.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetMouseButtonUp(0) && heldObject != null)
        {
            heldObject.isKinematic = false;

            // Check tag and reverse gravity for "Red" objects
            if (heldObject.CompareTag("Red"))
            {
                heldObject.gravityScale = reverseGravityScale; // Reverse gravity
            }
            else
            {
                heldObject.gravityScale = 1f; // Normal gravity for other objects
            }

            heldObject = null;
        }
    }
}
