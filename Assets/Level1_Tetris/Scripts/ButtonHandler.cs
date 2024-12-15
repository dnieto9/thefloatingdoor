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

    public GameObject[] objectsToFreeze; // Array to hold all objects that need to freeze

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

        // Disable grabbing
        if (grabberObject != null)
        {
            grabberObject.GetComponent<PlayerGrab>().enabled = false;
        }

        // Enable player movement
        if (playerObject != null)
        {
            playerObject.GetComponent<PlayerMovement>().enabled = true;
        }

        // Freeze all objects
        foreach (GameObject obj in objectsToFreeze)
        {
            Rigidbody2D rb2D = obj.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.isKinematic = true;
                rb2D.velocity = Vector2.zero;
                rb2D.angularVelocity = 0f;
            }

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
