using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public Transform[] spriteArrayPos; // Target positions
    public Rigidbody2D[] currentSpritePos; // Sprites' current positions

    void Start()
    {
       RandomizePiecePositions();

    }


    // Update is called once per frame
    void Update()
    {
        Overlapping();
    }

    // Check if the current positions are close to the target positions
    bool Overlapping()
    {
        bool isAnyOverlapping = false;
        float snapThreshold = 0.5f; // Define the distance threshold for snapping

        for (int i = 0; i < currentSpritePos.Length; i++)
        {
            if (i < spriteArrayPos.Length)
            {
                float distance = Vector2.Distance(currentSpritePos[i].position, spriteArrayPos[i].position);

                if (distance <= snapThreshold)
                {
                    // Snap the Rigidbody2D to the corresponding Transform position
                    currentSpritePos[i].position = spriteArrayPos[i].position;
                    
                    // Disable Rigidbody2D movement attributes to keep it in place
                    currentSpritePos[i].velocity = Vector2.zero;
                    //currentSpritePos[i].bodyType = RigidbodyType2D.Static;

                    isAnyOverlapping = true;
                }
            }
        }

        return isAnyOverlapping;
    }

    private void RandomizePiecePositions()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        foreach (Rigidbody2D piece in currentSpritePos)
        {
            float randomX = UnityEngine.Random.Range(screenLeft, screenRight);
            float randomY = UnityEngine.Random.Range(screenBottom, screenTop);
            piece.position = new Vector2(randomX, randomY);
        }

    }
    private void puzzleComplete(){
        
    }


}
