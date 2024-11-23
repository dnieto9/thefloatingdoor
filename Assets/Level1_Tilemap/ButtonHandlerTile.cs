using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandlerTile : MonoBehaviour
{
    public GameObject RulesPanel;
    public GameObject WifePanel;
    public GameObject SpyPanel;

    // Start is called before the first frame update
    public void Start()
    {
        WifePanel.SetActive(true);
        RulesPanel.SetActive(true);
    }

    public void OKButton()
    {
        RulesPanel.SetActive(false);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OkWife()
    {
        WifePanel.SetActive(false);
    }
    public void OkSpy()
    {
        SpyPanel.SetActive(false);
    }
}
