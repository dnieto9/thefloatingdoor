using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start(){
        volumeSlider.value = AudioManager.Instance.volume;
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }
    private void OnVolumeChanged(float value){
        AudioManager.Instance.SetVolume(value);
    }
}
