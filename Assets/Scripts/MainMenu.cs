using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Vilela");
        AudioManager.instance.PlaySound("Fight_Song1");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
