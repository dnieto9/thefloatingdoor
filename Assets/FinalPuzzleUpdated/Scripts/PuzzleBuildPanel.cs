using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBuildPanel : MonoBehaviour
{

    public RectTransform buildPanelRect; // The build panel where pieces will be placed
    public GameObject piecePrefab; // Prefab for the puzzle piece (use the same prefab for all pieces)
    public int gridSize = 4; // Number of rows and columns for the puzzle

    private Vector3[,] piecePositions; // Store positions as Vector3

    void Start()
    {
        InitializeBuildPanel();
    }

    public void InitializeBuildPanel()
    {
        // Calculate the size of the build panel based on the number of pieces and their size
        float pieceWidth = piecePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float pieceHeight = piecePrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        // Adjust the size of the panel to fit the puzzle grid
        float panelWidth = pieceWidth * gridSize;
        float panelHeight = pieceHeight * gridSize;

        buildPanelRect.sizeDelta = new Vector2(panelWidth, panelHeight);

        // Set the positions of the pieces within the panel
        piecePositions = new Vector3[gridSize, gridSize];
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                float xPosition = col * pieceWidth - (panelWidth / 2) + (pieceWidth / 2);
                float yPosition = row * pieceHeight - (panelHeight / 2) + (pieceHeight / 2);

                piecePositions[row, col] = new Vector3(xPosition, yPosition, 0);
            }
        }

        // Optionally, instantiate pieces at the calculated positions for testing
        CreatePuzzlePieces();
    }

    void CreatePuzzlePieces()
    {
        // Loop through grid positions and instantiate puzzle pieces at the calculated positions
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                GameObject piece = Instantiate(piecePrefab, piecePositions[row, col], Quaternion.identity);
                piece.transform.SetParent(buildPanelRect, false); // Attach piece to the panel
                piece.GetComponent<BoxCollider2D>().enabled = true; // Enable collider for drag and drop
            }
        }
    }
}

