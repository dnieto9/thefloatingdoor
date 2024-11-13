using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Piece2 : MonoBehaviour
{
    public Vector3 correctLoc;
    private Vector3 offset;
    private bool isDragging;
    private bool inPlace = false;
    private bool connected = false;
    private float snapThreshold = 0.5f; // Threshold distance to snap into place

    private int originalSortingOrder; // Stores the original sorting order
    private SpriteRenderer spriteRenderer;

    private Transform overlappingPiece;
    private List<Piece2> connectedPieces = new List<Piece2>(); // Use a list to store connected pieces
    // Start is called before the first frame update
    void Start()
    {
        correctLoc = transform.position;
        SetToRandomLocation();

        // Get the SpriteRenderer component and store the original sorting order
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSortingOrder = spriteRenderer.sortingOrder;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            correctOthersLoc(); // Update position of connected pieces
        }



    }

    void OnMouseDown()
    {
        // If the piece is already in place, disable dragging
      //  if (inPlace) return;

        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;


    }

    void OnMouseDrag()
    {
        // If the piece is already in place, disable dragging
      //  if (inPlace || !isDragging) return;

        transform.position = GetMouseWorldPosition() + offset;

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



    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if this piece is overlapping with another piece
        if (other.CompareTag("Piece"))
        {
            //  isOverlapping = true;
            Debug.Log("Overlapping with another piece.");

            overlappingPiece = other.transform;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Piece"))
        {
            // isOverlapping = false;
            Debug.Log("No longer overlapping.");
            overlappingPiece = null;
        }
    }
   
    private void SetToRandomLocation() ///dont mess with this
    {// Get the screen bounds in world coordinates
        Camera cam = Camera.main;
        float pieceWidth = GetComponent<Renderer>().bounds.size.x / 2;
        float pieceHeight = GetComponent<Renderer>().bounds.size.y / 2;

        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        // Generate random x and y positions within the screen bounds, accounting for piece size
        float randomX = UnityEngine.Random.Range(screenBottomLeft.x + pieceWidth, screenTopRight.x - pieceWidth);
        float randomY = UnityEngine.Random.Range(screenBottomLeft.y + pieceHeight, screenTopRight.y - pieceHeight);

        // Set the piece position to the random location
        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    private void correctOthersLoc()
    {
        //connectedPieces // for loop and change location to relative to 
        for (int i = 0; i< connectedPieces.Length; i++)
        {
           float correct_distance = Vector3.Distance(correctLoc, other.correctLoc);

            connectedPieces[i].transform.position = transform.position + correct_distance;
        } 
    }

    private void checkInPlace(Piece2 other) /// check if connected with other
    {
        // Calculate the distance between the current position and the correct location
        float curr_distance = Vector3.Distance(transform.position, other.transform.position);
        float correct_distance = Vector3.Distance(correctLoc, other.correctLoc);
        //Debug.Log(distance);
        if (curr_distance.abs() <= correct_distance.abs())
        {
            connectedPieces += other;
            connected = true;
            other.connect;

        }
    }
    private void connect(){
        return connected = true;
    }

    public bool done()
    {
        return inPlace;
    }
}
