using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab2 : MonoBehaviour
{
    private Rigidbody2D heldObject;
    private Vector3 mouseOffset;
    public float rotationSpeed = 100f;

    void Start()
    {
        foreach (Rigidbody2D rb in FindObjectsOfType<Rigidbody2D>())
        {
            if (rb.CompareTag("Ground")) continue;
            ObjectSoundHandler soundHandler = rb.GetComponent<ObjectSoundHandler>();
            if (soundHandler != null && soundHandler.isLocked) continue;

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

                ObjectSoundHandler soundHandler = hit.collider.GetComponent<ObjectSoundHandler>();
                if (soundHandler != null && soundHandler.isLocked) return;

                heldObject = hit.collider.attachedRigidbody;
                mouseOffset = heldObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Notify the object that it's being held
                if (soundHandler != null)
                {
                    soundHandler.SetHeld(true);
                }

                // Freeze rotation while grabbing
                heldObject.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            // Notify the object that it's no longer being held
            ObjectSoundHandler soundHandler = heldObject.GetComponent<ObjectSoundHandler>();
            if (soundHandler != null)
            {
                soundHandler.SetHeld(false);
            }

            heldObject.isKinematic = false;

            // Restore original constraints
            heldObject.constraints = RigidbodyConstraints2D.None;
            heldObject = null;
        }
    }
}
