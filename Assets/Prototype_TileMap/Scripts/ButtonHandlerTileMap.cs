using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlerTileMap : MonoBehaviour
{
    public GameObject panel;
    public GameObject SpyPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        panel.SetActive(false);
        SpyPanel.SetActive(true);
    }
}
