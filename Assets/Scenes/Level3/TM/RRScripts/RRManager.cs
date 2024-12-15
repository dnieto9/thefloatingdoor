using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RRManager : MonoBehaviour
{
    public TMP_InputField pianoNotes;
    private string notes = "daeadcfgbd";
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            ValidateCode();
        }
    }

    public void ValidateCode(){
        if (pianoNotes.text == notes){
            Debug.Log("correct!");
            //activate puzzle
        }else{
            pianoNotes.text = "";
        }
    }
}
