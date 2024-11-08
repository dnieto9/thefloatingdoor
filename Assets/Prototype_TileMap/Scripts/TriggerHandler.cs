using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameObject panel;

    public void OnTriggerEnter2D(Collider2D other)
    {
        panel.SetActive(true);
    }
}
