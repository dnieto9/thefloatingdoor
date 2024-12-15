using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCredits : MonoBehaviour
{
    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI titleText;  // New TextMeshProUGUI for the game title
    public Button mainMenuButton;
    public float delayBetweenLines = 2f;
    private string[] creditLines = new string[]
     {
        "Thank you for playing our game!",
        "This game was created by Daisy Nieto, Akshaya Ranjit, and Gordey Danilochkin.",
        "Special thanks to Dr. Britton Horn for your invaluable support.",
        "Sound and music were created by Emir Jonsson.",
        "A special thank you to Caroline Meehan for her help identifying artworks <3",
        "During development, assets from Penzilla, Everly's Pixels and Pens, and SierraAssets were used.",
        "A big thank you to our playtesters for your feedback and support.",
        "^_^"
     };


    void Start()
    {
        if (creditsText != null)
        {
            StartCoroutine(DisplayCredits());
        }
    }

    private IEnumerator DisplayCredits()
    {
        foreach (string line in creditLines)
        {
            creditsText.text += line + "\n\n"; // Add the new line to the existing text
            yield return new WaitForSeconds(delayBetweenLines); // Wait for the specified time
        }
    }
}
