using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public GameObject grabberObject;
    public GameObject playerObject;

    public GameObject RealLego;
    public GameObject LegoSprite;
    // Start is called before the first frame update
    public void Start()
    {
        playerObject.GetComponent<PlayerMovement>().enabled = false;
        RealLego.SetActive(false);
        LegoSprite.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FinishedBuilding()
    {
        RealLego.SetActive(true);
        LegoSprite.SetActive(false);
        if (grabberObject != null)
        {
            grabberObject.GetComponent<PlayerGrab>().enabled = false;
        }

        if (playerObject != null)
        {
            playerObject.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
