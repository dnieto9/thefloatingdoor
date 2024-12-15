using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject sound; // Reference to the AudioSource component
    public Toggle audioToggle; // Reference to the UI Toggle
    public Slider audioSlider; // Reference to the UI Slider
    private AudioSource audioControls;
   void Start()
{
    // Get the AudioSource component from the assigned GameObject
    audioControls = sound.GetComponent<AudioSource>();
    if (audioControls == null)
    {
        Debug.LogError("No AudioSource component found on the 'sound' GameObject.");
        return;
    }

    // Initialize toggle and slider
    audioToggle.isOn = audioControls.volume > 0; // Set toggle state based on current volume
    audioSlider.value = audioControls.volume; // Set slider position based on current volume

    // Add listeners to toggle and slider
    audioToggle.onValueChanged.AddListener(OnAudioToggleChanged);
    audioSlider.onValueChanged.AddListener(OnAudioSliderChanged);
}


    // Toggle event handler
    void OnAudioToggleChanged(bool isOn)
    {
        if (isOn)
        {
            audioControls.volume = audioSlider.value; // Restore volume to slider's current value
        }
        else
        {
            audioControls.volume = 0; // Mute the audio
        }
    }

    // Slider event handler
    void OnAudioSliderChanged(float value)
    {
        if (audioToggle.isOn)
        {
            audioControls.volume = value; // Adjust volume only if the toggle is on
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype_TileMap");
    }
}
