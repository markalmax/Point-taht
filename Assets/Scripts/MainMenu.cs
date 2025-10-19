using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void levelsMenu()
    {
        
    }
    public void InfoMenu()
    {

    }
    public void BackMenu()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
