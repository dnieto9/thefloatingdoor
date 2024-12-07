using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Puzzle : MonoBehaviour
{
   public PuzzleBuildPanel buildPanel; // Reference to the new build panel

    public Sprite sourceSprite; // The sprite to break into pieces
    public Sprite finalSprite; // Final sprite to show
    public int gridSize = 4; // Number of rows and columns
    public RectTransform panelRect; // Panel to confine pieces
    public Button cont;

    private Sprite[,] puzzlePieces; // Array to hold the sprite pieces
    private GameObject[,] pieceObjects; // Array to hold the GameObjects for pieces
    private Vector3[,] puzzleOrigins; // Store positions as Vector3

    private bool finalSpriteDisplayed = false; // Flag to check if the final sprite has been displayed

    void Start()
    {
        
        Debug.Log("Starting");

        if (sourceSprite == null)
        {
            Debug.LogError("No sprite assigned");
            return;
        }
        if (panelRect == null)
        {
            Debug.LogError("No panel assigned!");
            return;
        }

        // Initialize arrays
        puzzlePieces = new Sprite[gridSize, gridSize];
        pieceObjects = new GameObject[gridSize, gridSize];
        puzzleOrigins = new Vector3[gridSize, gridSize];

        CreatePuzzle();
    }

    void Update()
    {
        CheckPuzzleCompletion();
    }

    // Other methods like CreatePuzzle(), CheckPuzzleCompletion(), etc. remain unchanged
private void CreatePuzzle()
{
    // Use texture of the source sprite
    Texture2D texture = sourceSprite.texture;
    int pieceWidth = texture.width / gridSize; // Width in pixels
    int pieceHeight = texture.height / gridSize; // Height in pixels

    float pieceWorldWidth = sourceSprite.bounds.size.x / gridSize; // Width in world units
    float pieceWorldHeight = sourceSprite.bounds.size.y / gridSize; // Height in world units

    float xOffset = -sourceSprite.bounds.size.x / 2 + pieceWorldWidth / 2;
    float yOffset = -sourceSprite.bounds.size.y / 2 + pieceWorldHeight / 2;

    for (int row = 0; row < gridSize; row++)
    {
        for (int col = 0; col < gridSize; col++)
        {
            // Create a rect for this piece in pixel coordinates
            Rect spriteRect = new Rect(col * pieceWidth, row * pieceHeight, pieceWidth, pieceHeight);
            Sprite pieceSprite = Sprite.Create(texture, spriteRect, new Vector2(0.5f, 0.5f));

            // Create the piece GameObject
            GameObject gamePiece = new GameObject($"Piece_{row}_{col}");
            gamePiece.transform.parent = this.transform;

            // Add the SpriteRenderer and assign the sprite
            SpriteRenderer sr = gamePiece.AddComponent<SpriteRenderer>();
            sr.sprite = pieceSprite;

            // Set the sorting order (optional, e.g., 5)
            sr.sortingOrder = 5;

            // Add the BoxCollider2D
            BoxCollider2D collider = gamePiece.AddComponent<BoxCollider2D>();

            // Add draggable behavior
            gamePiece.AddComponent<Draggable>();

            // Calculate the piece's position in world space
            float xPosition = xOffset + col * pieceWorldWidth;
            float yPosition = yOffset + row * pieceWorldHeight;

            // Store the expected position
            puzzleOrigins[row, col] = new Vector3(xPosition, yPosition, 0);

            // Place the piece at a random position within the panel
            gamePiece.transform.position = GetRandomPositionWithinPanel(new Vector2(pieceWorldWidth, pieceWorldHeight));

            // Scale the piece to match its world size
            float scaleX = pieceWorldWidth / pieceSprite.bounds.size.x;
            float scaleY = pieceWorldHeight / pieceSprite.bounds.size.y;
            gamePiece.transform.localScale = new Vector3(scaleX, scaleY, 1);

            // Store the GameObject
            pieceObjects[row, col] = gamePiece;
        }
    }
}

 Vector3 GetRandomPositionWithinPanel(Vector2 pieceWorldSize)
    {
       // Get the world corners of the panel
        Vector3[] corners = new Vector3[4];
        panelRect.GetWorldCorners(corners);

        // Calculate adjusted bounds for pieces to fit entirely inside the panel
        float minX = corners[0].x + pieceWorldSize.x / 2f;
        float maxX = corners[2].x - pieceWorldSize.x / 2f;
        float minY = corners[0].y + pieceWorldSize.y / 2f;
        float maxY = corners[1].y - pieceWorldSize.y / 2f;

        // Generate a random position within these bounds
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        return new Vector3(randomX, randomY, 0);
    }
    public void CheckPuzzleCompletion2()
    {
        bool isPuzzleComplete = true;

        // Loop through the grid to check each piece
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                // Get the expected position for the current piece
                Vector3 expectedPosition = puzzleOrigins[row, col];

                // Get the actual position of the current piece
                GameObject piece = pieceObjects[row, col];
                Vector3 actualPosition = piece.transform.position;

                // Check if the piece is within a certain distance of its expected position
                float distance = Vector3.Distance(actualPosition, expectedPosition);
                if (distance > 0.5f) // Increased tolerance threshold
                {
                    isPuzzleComplete = false;
                    break; // No need to check further if one piece is incorrect
                }
            }

            if (!isPuzzleComplete)
                break; // Exit early if puzzle is not complete
        }

        // If the puzzle is complete, hide the pieces and show the assembled sprite
        if (isPuzzleComplete && !finalSpriteDisplayed)
        {
            Debug.Log("Assembling Puzzle"); // Make sure this statement prints

            AssemblePuzzle();  // This hides the pieces

            // Now show the final sprite
            if (finalSprite != null)
            {
                ShowFinalSprite();
                finalSpriteDisplayed = true; // Set flag to true once the final sprite is displayed
            }

            // Optionally, enable the continue button
            if (cont != null)
            {
                cont.gameObject.SetActive(true); // Show the continue button
                cont.interactable = true; // Enable interaction with the button
            }
        }
    }


public void CheckPuzzleCompletion()
{
    bool isPuzzleComplete = true;

    // Define a tolerance based on piece size (can be adjusted for more or less precision)
    float tolerance = Mathf.Max(pieceObjects[0, 0].GetComponent<SpriteRenderer>().bounds.size.x, 
                                pieceObjects[0, 0].GetComponent<SpriteRenderer>().bounds.size.y) * 0.5f;

    // Loop through the grid to check each piece
    for (int row = 0; row < gridSize; row++)
    {
        for (int col = 0; col < gridSize; col++)
        {
            // Get the expected position for the current piece
            Vector3 expectedPosition = puzzleOrigins[row, col];

            // Get the actual position of the current piece
            GameObject piece = pieceObjects[row, col];
            Vector3 actualPosition = piece.transform.position;

            // Check if the piece is within the tolerance distance of its expected position
            float distance = Vector3.Distance(actualPosition, expectedPosition);

            if (distance > 1f) // Use dynamic tolerance //tolerance
            {
                isPuzzleComplete = false;
                break; // No need to check further if one piece is incorrect
            }
        }

        if (!isPuzzleComplete)
            break; // Exit early if puzzle is not complete
    }

    // If the puzzle is complete, hide the pieces and show the assembled sprite
    if (isPuzzleComplete && !finalSpriteDisplayed)
    {
        Debug.Log("Assembling Puzzle");

        AssemblePuzzle();  // This hides the pieces

        // Now show the final sprite
        if (finalSprite != null)
        {
            ShowFinalSprite();
            finalSpriteDisplayed = true; // Set flag to true once the final sprite is displayed
        }

        // Optionally, enable the continue button
        if (cont != null)
        {
            cont.gameObject.SetActive(true); // Show the continue button
            cont.interactable = true; // Enable interaction with the button
        }
    }
}

    private void ShowFinalSprite()
    {
        // Create a new GameObject to display the final sprite
        GameObject finalPuzzle = new GameObject("FinalPuzzle");

        // Add a SpriteRenderer to display the final sprite
        SpriteRenderer sr = finalPuzzle.AddComponent<SpriteRenderer>();
        sr.sprite = finalSprite;
        sr.sortingOrder = 5;

        // Optionally, position the final sprite in the correct location
        finalPuzzle.transform.position = new Vector3(0, 0, 0); // Set position as needed

        // You can also disable dragging or add other functionality to the final sprite if necessary
    }

    private void AssemblePuzzle()
    {
        Debug.Log("Hiding Puzzle Pieces");

        // Hide all the pieces
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                GameObject piece = pieceObjects[row, col];
                piece.SetActive(false); // Hide the piece
            }
        }

        Debug.Log("Puzzle Completed and Assembled");
    }

}
