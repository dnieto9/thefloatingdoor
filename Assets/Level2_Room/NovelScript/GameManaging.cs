using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;



public class GameManaging : MonoBehaviour
{
    public Light2D global;
    public Light2D playerLight;
    public Collider2D compTrigger;
    //public UnityOnTriggerEnter2DMessageListener compTrigger2;
    // Start is called before the first frame update
    void Start()
    {
        global.gameObject.SetActive(false);
        playerLight.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("player triggered event");
    }
}
