using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectSoundHandler : MonoBehaviour
{
    public AudioSource impactSound; // Assign this in the Inspector
    public bool isLocked = false; // To prevent interaction with locked objects

    private bool isHeld = false;

    void Start()
    {
        if (impactSound == null)
        {
            impactSound = gameObject.AddComponent<AudioSource>();
        }
    }

    // Call this when the object is grabbed
    public void SetHeld(bool held)
    {
        isHeld = held;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHeld) // Play sound only when the object is not held
        {
            if (impactSound != null)
            {
                impactSound.Play();
            }
        }
    }
}
