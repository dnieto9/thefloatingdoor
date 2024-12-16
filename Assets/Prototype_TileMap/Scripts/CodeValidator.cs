using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class CodeValidator : MonoBehaviour
{
    public TMP_InputField codeInputField; 
    public string secretCode = "4316";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ValidateCode();
        }
    }


    public void ValidateCode()
    {
        if (codeInputField.text == secretCode)
        {
            Debug.Log(codeInputField.text);
            SceneManager.LoadScene("ClockPuzzle");

        }
        else
        {
            codeInputField.text = ""; 
        }
    }

    
}

