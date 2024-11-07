using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragging : MonoBehaviour
{
    private Vector3 offset;
    public bool isDragging = false;
    

    void OnMouseDown()
    {
        // Calculate offset between mouse position and object position
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Move object to the new mouse position with offset
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in screen coordinates and convert it to world coordinates
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z; // Keep same z-depth for 3D or use 0 for 2D
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
