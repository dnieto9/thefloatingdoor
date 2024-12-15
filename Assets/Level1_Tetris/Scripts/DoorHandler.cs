using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    public GameObject closedDoor;
    public GameObject openDoor;
    public GameObject panel;
    public float waitTime = -1000f;
    public Rigidbody2D playerRigidbody;

    public AudioSource doorOpenSound; // Reference to the door open sound

    private bool isTransitioning = false;
    private RigidbodyConstraints2D originalConstraints; // To restore original constraints later

    private void Start()
    {
        // Ensure doorOpenSound is assigned or added
        if (doorOpenSound == null)
        {
            doorOpenSound = gameObject.AddComponent<AudioSource>();
        }

        if (playerRigidbody != null)
        {
            // Save the original constraints for restoration
            originalConstraints = playerRigidbody.constraints;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isTransitioning)
        {
            isTransitioning = true;
            StartCoroutine(HandleDoorTransition(other.gameObject));
        }
    }

    private IEnumerator HandleDoorTransition(GameObject player)
    {
        if (playerRigidbody != null)
        {
            // Stop player movement
            playerRigidbody.velocity = Vector2.zero;

            // Freeze the player's position
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;

            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
        }

        if (closedDoor != null)
        {
            SpriteRenderer closedDoorRenderer = closedDoor.GetComponent<SpriteRenderer>();
            if (closedDoorRenderer != null)
            {
                closedDoorRenderer.sortingOrder = -2;
            }
        }

        if (openDoor != null)
        {
            openDoor.SetActive(true);
        }

        // Play the door opening sound
        if (doorOpenSound != null)
        {
            doorOpenSound.Play();
        }

        yield return new WaitForSeconds(waitTime);

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Prototype_Tetris")
        {
            SceneManager.LoadScene("Gadgets");
        }
        else if (currentScene == "Level2_Tetris")
        {
            SceneManager.LoadScene("Level3_Tetris");
        }
        else if (currentScene == "Level3_Tetris")
        {
            SceneManager.LoadScene("EndCredits");
        }

        if (playerRigidbody != null)
        {
            // Restore the original constraints
            playerRigidbody.constraints = originalConstraints;
        }

        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }
    }
}
