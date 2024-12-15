using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArtworkInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform piece;
    public GameObject info;
    public LayerMask Player;
    private bool explicitlyAsked = false;
    void Start()
    {
        info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(piece.position, .5f, Player) && !explicitlyAsked){
            //Debug.Log("overlap exists");
            info.SetActive(true);
            //Debug.Log("info should be set active");
        }else if (!Physics2D.OverlapCircle(piece.position, .5f, Player)){
            explicitlyAsked = false;
            info.SetActive(false);
        }
    }

    public void close(){
        explicitlyAsked = true;
        info.SetActive(false);
    }

}
