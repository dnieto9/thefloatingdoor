using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Range(0,1)] public float volume = 1f;
    
     private void Awake() {
        
        if(Instance == null){
            Instance= this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

    }

    public void SetVolume(float newVolume){
        volume = newVolume;
        AudioListener.volume = volume;
    }
}
