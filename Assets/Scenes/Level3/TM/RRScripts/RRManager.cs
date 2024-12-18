using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RRManager : MonoBehaviour
{
    public TMP_InputField pianoNotes;
    // public GameObject pPuzz;
    // public GameObject pPuzzMan;
    
    private bool pPuzzOn = false;
    
    private string notes = "daeadcfgbd";
    public Rigidbody2D playerRB;
    public GameObject player;

    public BoxCollider2D playerColl;
    // Start is called before the first frame update

    void Start(){
        // pPuzz.SetActive(false);
        // pPuzzMan.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            ValidateCode();
        }

        // if(pPuzzOn){
        //     playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        //     playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
        //     playerColl.enabled = false;
        //     player.SetActive(false);
        // }else{
        //     playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        //     playerColl.enabled = true;
        //     player.SetActive(true);
        // }
    }

    public void ValidateCode(){
        if (pianoNotes.text == notes){
            Debug.Log("correct!");
            // pPuzz.SetActive(true);
            // pPuzzMan.SetActive(true);
            SceneManager.LoadScene("Level3LongPiano");
            pPuzzOn = true;
        }else{
            pianoNotes.text = "";
        }
    }

    public void LoadLanternPuzz(){
        Debug.Log("should load Level3Lamp");
        SceneManager.LoadScene("Level3Lamp");
    }

    public void mainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    
}
