/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; // Import SceneManager


public class PuzzleManager1 : MonoBehaviour
{
   public GameObject[] pieces;
   public GameObject restart;
    public GameObject cont;
    // Start is called before the first frame update
    public string nextSceneName; // Add the name of the next scene here
    private bool puzzlesDone;

    void Start()
    {





    }

    // Update is called once per frame
    void Update()
    {
        puzzlesDone = isdone();

        if (puzzlesDone)
        {
            cont.SetActive(true);
            restart.SetActive(false);
        }

    }

    private bool isdone()
    {

        int count = 0;
        int total = pieces.Length;

        for (int i = 0; i < pieces.Length; i++)
        {
            // Check if the piece has the script 'YourScriptName' attached
            if (pieces[i].GetComponent<Piece>() != null && pieces[i].GetComponent<Piece>().done())
            {
                count++;
            }
        }

        return (count == total);


    }

    public void puzzleComplete()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void restartScene()
    {
        SceneManager.LoadScene("chairtemp");

    }
}*/