using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Intro_Map");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("New_MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
