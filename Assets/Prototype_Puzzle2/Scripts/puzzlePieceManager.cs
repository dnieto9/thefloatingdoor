using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; // Import SceneManager

public class puzzlePieceManager : MonoBehaviour
{
    public GameObject[] pieces;
   // public GameObject[] puzzle2;
    //public GameObject panel; // Reference to the UI panel

    public GameObject restart;
    public GameObject cont;
    // Start is called before the first frame update
    public string nextSceneName; // Add the name of the next scene heres
    private bool puzzlesDone;
   // private bool puzzlesDone2;

    public string currentScene;



    void Start()
    {
        cont.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        puzzlesDone = isdone(pieces);
      //  puzzlesDone2 = isdone(puzzle2);


        if (puzzlesDone)// & puzzlesDone2)
        {
            cont.SetActive(true);
            restart.SetActive(false);
        }

    }

    private bool isdone(GameObject[] puzzle)
    {

        int count = 0;
        int total = puzzle.Length;//pieces.Length;
        bool connects = false;

        for (int i = 0; i < puzzle.Length;i++)//pieces.Length; i++)
        {
            // Check if the piece has the script 'YourScriptName' attached
            if (puzzle[i].GetComponent<puzzlePiece>() != null && puzzle[i].GetComponent<puzzlePiece>().done())
            {
                count++;
                if(puzzle[i].GetComponent<puzzlePiece>().numConnections() >= total-1){
                    connects = true;
                }
            }
        }

           // bool connects = pieces[0].GetComponent<puzzlePiece>().numConnections() == total-1;

            bool ret = connects & (count == total);


        return ret;//(count == total);


    }

    
    public void puzzleComplete()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void restartScene()
    {
        SceneManager.LoadScene(currentScene);

    }
}
