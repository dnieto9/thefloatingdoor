using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class puzzlePiece : MonoBehaviour
{
    private Vector3 correctLoc;
    public RectTransform panelRect; // Drag your panel here in the Inspector

    private Vector3 offset;
    private bool isDragging;
    private bool inPlace = false;
    private bool connected = false;
    public string puzzName;
    //private int originalSortingOrder; // Stores the original sorting order
    private SpriteRenderer spriteRenderer;

   private  List<puzzlePiece> connectedPieces = new List<puzzlePiece>();


    void Start()
    {
        correctLoc = transform.position;
        //Debug.Log(correctLoc);
        SetToRandomLocation();

        // Get the SpriteRenderer component and store the original sorting order
     /*   spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSortingOrder = spriteRenderer.sortingOrder;
        }*/
    }



    // Update is called once per frame
    void Update()
    {
         if (connected & isDragging)
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

    void OnMouseDrag(){
         // Calculate the new position based on mouse position and offset
    Vector3 newPosition = GetMouseWorldPosition() + offset;

    // Clamp the new position to stay within screen bounds
    Vector3 clampedPosition = ClampToScreenBounds(newPosition);

    // Update the object's position
    transform.position = clampedPosition;
    }

    private Vector3 ClampToScreenBounds(Vector3 position)
    {
     RectTransform panelRect = transform.parent.GetComponent<RectTransform>();
    Rect panelBounds = panelRect.rect;

    // Clamp position within panel bounds
    position.x = Mathf.Clamp(position.x, panelBounds.xMin, panelBounds.xMax);
    position.y = Mathf.Clamp(position.y, panelBounds.yMin, panelBounds.yMax);

    return position;
    }


    // void OnMouseDrag()
    // {


    //     transform.position = GetMouseWorldPosition() + offset;

    // }



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
            //Debug.Log("Overlapping with another piece.");

            puzzlePiece pieceScript = other.GetComponent<puzzlePiece>();

            if(pieceScript.puzzName == this.puzzName){
                checkInPlace(pieceScript);

            }


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Piece"))
        {
            // isOverlapping = false;
            //Debug.Log("No longer overlapping.");


         //   overlappingPiece = null;
        }
    }
   
 private void SetToRandomLocation()
{
    if (panelRect == null)
    {
        //Debug.LogError("Panel RectTransform is not assigned!");
        return;
    }

    // Get the local bounds of the panel
    Rect panelBounds = panelRect.rect;

    // Generate random positions within these bounds
    float randomX = UnityEngine.Random.Range(panelBounds.xMin, panelBounds.xMax);
    float randomY = UnityEngine.Random.Range(panelBounds.yMin, panelBounds.yMax);

    // Set position, converting from local to world space
    Vector3 localPos = new Vector3(randomX, randomY, transform.position.z);
    transform.position = panelRect.TransformPoint(localPos);
}

    private void correctOthersLoc()
    {
    
     // Loop through each connected piece and set its position relative to the current piece
     for (int i = 0; i < connectedPieces.Count; i++)
     {
        // Calculate the correct offset between the connected piece's correct location and this piece's correct location
         Vector3 offset = connectedPieces[i].correctLoc - correctLoc;
        
        // Position the connected piece based on the offset
         connectedPieces[i].transform.position = transform.position + offset;
     }

    }

    private void checkInPlace(puzzlePiece other) /// check if connected with other
    {
        
RectTransform thisRect = GetComponent<RectTransform>();
    RectTransform otherRect = other.GetComponent<RectTransform>();

    // Calculate the difference in anchored positions
    Vector2 positionDifference = thisRect.anchoredPosition - otherRect.anchoredPosition;
    Vector2 correctDifference = correctLoc - other.correctLoc;

    float tolerance = 10f; // Adjust for UI space

    // Check if the pieces are close enough and aligned
    if (Mathf.Abs(positionDifference.x - correctDifference.x) <= tolerance &&
        Mathf.Abs(positionDifference.y - correctDifference.y) <= tolerance)
    {
        connectedPieces.Add(other);
        other.addConnections(this);
        connected = true;
        inPlace = true;
    }

    }

    private Vector3 ClampToPanelBounds(Vector3 position)
{
    if (panelRect == null)
    {
        //Debug.LogError("Panel RectTransform is not assigned!");
        return position;
    }

    // Get the bounds of the panel
    Vector2 panelMin = panelRect.rect.min;
    Vector2 panelMax = panelRect.rect.max;

    // Convert local panel bounds to world space
    Vector3 worldMin = panelRect.TransformPoint(panelMin);
    Vector3 worldMax = panelRect.TransformPoint(panelMax);

    // Clamp the position to stay within the panel
    position.x = Mathf.Clamp(position.x, worldMin.x, worldMax.x);
    position.y = Mathf.Clamp(position.y, worldMin.y, worldMax.y);

    return position;
}
  private bool IsWithinPanelBounds(RectTransform panelRect, RectTransform pieceRect)
{
    Rect panelBounds = panelRect.rect;
    Rect pieceBounds = pieceRect.rect;

    return panelBounds.Overlaps(pieceBounds);
}

    public bool done()
    {
        return inPlace;
    }

    public List<puzzlePiece> connections(){
        return connectedPieces;
    }

    public void addConnections(puzzlePiece other){
        connectedPieces.Add(other);
    }

    public int numConnections(){
        return connectedPieces.Count;
    }
         
}
