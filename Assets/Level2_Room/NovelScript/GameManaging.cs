using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManaging : MonoBehaviour
{
    public Transform player;
    public Light2D global;
    public Light2D playerLight;
    public Collider2D compTrigger;
    public SpriteRenderer closetIcon;
    public LayerMask ClosetPcsLM;
    public GameObject theClosetPcs;
    public Button closetBuilder;
    public Button nextStep;
    public GameObject closetPuzz;
    int gadgetNum;
    public SpriteRenderer gadgIcon;
    int toolsNum;
    public GameObject gadgetPuzz;
    public Button gadgetBuilder;
    public GameObject startingPan;

    //public UnityOnTriggerEnter2DMessageListener compTrigger2;
    // Start is called before the first frame update
    void Start()
    {
        global.gameObject.SetActive(false);
        playerLight.gameObject.SetActive(true);
        closetIcon.color = new Color (0,0,0,1);
        gadgIcon.color = new Color(0,0,0,1);
        closetBuilder.gameObject.SetActive(false);
        gadgetBuilder.gameObject.SetActive(false);
        closetPuzz.SetActive(false);
        gadgetPuzz.SetActive(false);

        nextStep.gameObject.SetActive(false);
        gadgetNum = 0;
        toolsNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.position);
        if(Physics2D.OverlapCircle(player.position, 2f, ClosetPcsLM)){
            closetIcon.color = new Color (0.75f,0.75f,0.75f,255);
            closetBuilder.gameObject.SetActive(true);
        }

        if(gadgetNum >=5){
            gadgetBuilder.gameObject.SetActive(true);
        }

        if(toolsNum >=2){
            nextStep.gameObject.SetActive(true);
        }
 
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("player triggered event");
    }

    public void puzzleVisible(){
        closetPuzz.SetActive(true); 
        theClosetPcs.SetActive(false);
    }

    public void loadTetris(){
        SceneManager.LoadScene("Level2_Tetris");
    }

    public void closetFinished(){
        toolsNum +=1;
        theClosetPcs.SetActive(false);
        closetPuzz.SetActive(false);
        closetBuilder.gameObject.SetActive(false);
        closetIcon.color = new Color (255, 255, 255,255);
    }

    public void puzzleInvisible(){
        closetPuzz.SetActive(false);
        theClosetPcs.SetActive(true);
    }

    public void addToGadget(){
        gadgetNum += 1;
       // gadgIcon.color = new Color(gadgetNum*frac,gadgetNum*frac, gadgetNum*frac, 255);
        gadgIcon.color = new Color(gadgIcon.color.r + .2f, gadgIcon.color.b + .2f, gadgIcon.color.g + .2f, 255);
        Debug.Log(gadgIcon.color);
    }

    public void gadgetPuzzViz(){
        gadgetPuzz.SetActive(true);
    }

    public void gadgetPuzzInviz(){
        gadgetPuzz.SetActive(false);
    }

    public void gadgetPuzzFin(){
        toolsNum +=1;
        gadgetPuzz.SetActive(false);
        gadgetBuilder.gameObject.SetActive(false);
        gadgIcon.color = new Color(1,1,1,1);
    }

    public void gotIt(){
        startingPan.SetActive(false);
    }

    public void mainMendu(){
        SceneManager.LoadScene("MainMenu");
    }
}
