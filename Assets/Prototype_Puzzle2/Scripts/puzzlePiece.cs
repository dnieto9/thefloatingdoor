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
        Debug.Log(correctLoc);
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
        //     Debug.Log("HERE in Update");
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
            Debug.Log("No longer overlapping.");


         //   overlappingPiece = null;
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
        

// Calculate the difference between the current positions of the two pieces
    float xDifference = Mathf.Abs(transform.position.x - other.transform.position.x);
    float yDifference = Mathf.Abs(transform.position.y - other.transform.position.y);

    // Calculate the difference between the correct positions for alignment check
    float correctX = Mathf.Abs(correctLoc.x - other.correctLoc.x);
    float correctY = Mathf.Abs(correctLoc.y - other.correctLoc.y);

    // Define a small tolerance value for alignment
    float tolerance = 0.1f; 

    // Check if the pieces are close enough to each other and aligned correctly
    if (xDifference <= correctX + tolerance && yDifference <= correctY + tolerance)
    {
        // If aligned within the correct distance, add the other piece to the connected pieces list
        connectedPieces.Add(other);
        other.addConnections(this);
        connected = true;

        List<puzzlePiece> newConnects = other.connections();
        if(newConnects.Count != 0){
          for(int i = 0; i < newConnects.Count; i++) {
            connectedPieces.Add(newConnects[i]);
        }
        } 
        if(connectedPieces.Count != 0){
            for(int i = 0; i < connectedPieces.Count; i++) {
                other.addConnections(connectedPieces[i]);
       
        }
        }   
        other.connected = true;
        
        
        inPlace = true;
        
    }

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
