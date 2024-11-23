using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public LayerMask ClosetPcs;
    public GameObject finCloset;
    public Button closetBuilder;
    public GameObject closetPuzz;

    //public UnityOnTriggerEnter2DMessageListener compTrigger2;
    // Start is called before the first frame update
    void Start()
    {
        global.gameObject.SetActive(false);
        playerLight.gameObject.SetActive(true);
        closetIcon.color = new Color (0,0,0,1);
        closetBuilder.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.position);
        if(Physics2D.OverlapCircle(player.position, 2f, ClosetPcs)){
            closetIcon.color = new Color (255, 255 , 255 ,255);
            closetBuilder.gameObject.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("player triggered event");
    }

    public void puzzleVisible(){
        Debug.Log("puzzle should be working");
        closetPuzz.SetActive(true); 
    }

    public void loadTetris(){
        SceneManager.LoadScene("Prototype_Tetris");
    }

}
