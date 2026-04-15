using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public string menuSceneName = "MenuPrincipal"; 

    public void GoToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(menuSceneName);
    }
}