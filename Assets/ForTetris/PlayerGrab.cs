using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private Rigidbody2D heldObject;
    private Vector3 mouseOffset;
    public float rotationSpeed = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.attachedRigidbody != null)
            {
                if (hit.collider.CompareTag("UI")) return;
                if (hit.collider.CompareTag("Player")) return;

                heldObject = hit.collider.attachedRigidbody;
                mouseOffset = heldObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            heldObject = null;
        }
    }
}
