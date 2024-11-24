using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    public GameObject panel;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "Prototype_Tetris"){
                SceneManager.LoadScene("Gadgets");
            }
            else if (SceneManager.GetActiveScene().name == "Level2_Tetris"){
                panel.SetActive(true);
            }
            
        }
    }
}
