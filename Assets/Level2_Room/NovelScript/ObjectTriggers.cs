using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trigg;
    public GameObject ask;
    public LayerMask Player;
    public GameObject spotlight;
    public GameObject global;
    bool explicitlyAsked = false;

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(trigg.position, .5f, Player)){
                if(!explicitlyAsked){
                    ask.SetActive(true);
                }
        }else{
            ask.SetActive(false);
            explicitlyAsked = false;
        }
    }
    public void lightsOn(){
        explicitlyAsked = true;
        ask.SetActive(false);
        global.SetActive(true);
        spotlight.SetActive(false);
    }

    public void no(){
        explicitlyAsked = true;
        ask.SetActive(false);
    }

    

}
