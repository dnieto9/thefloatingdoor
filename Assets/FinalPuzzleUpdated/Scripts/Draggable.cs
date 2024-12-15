using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private float zCoordinate;

    void OnMouseDown()
    {
        zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
