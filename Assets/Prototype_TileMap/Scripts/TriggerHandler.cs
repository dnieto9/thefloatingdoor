using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameObject panel;
    private bool isPlayerInTriggerZone = false;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTriggerZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTriggerZone = false;
        }
    }
}
