using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetPcs : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform piece;
    public GameObject ask;
    public LayerMask Player;
    void Start()
    {
        ask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(piece.position, 2f, Player)){
            ask.SetActive(true);
        }else{
            ask.SetActive(false);
        }
    }

    public void addToGadget(){
        ask.SetActive(false);
        piece.gameObject.SetActive(false);
    }

    public void no(){
        ask.SetActive(false);
    }
}
