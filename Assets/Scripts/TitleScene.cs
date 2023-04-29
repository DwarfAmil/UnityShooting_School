using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public TMP_InputField inputField;

    public string saveData;

    public void InputFieldName()
    {
        saveData = inputField.text;
        PlayerPrefs.SetString("_PlayerName", saveData);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Name");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    
    public void OnClickNext()
    {
        SceneManager.LoadScene("Play");
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("Title");
    }

}
