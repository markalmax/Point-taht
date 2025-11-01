using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject main;
    public GameObject levels;  
    public GameObject info;
    public void StartGame()
    {
        Debug.Log("Start Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void levelsMenu()
    {
        Debug.Log("Levels Menu");
        main.SetActive(false);
        levels.SetActive(true);
    }
    public void InfoMenu()
    {
        Debug.Log("Info Menu");
        main.SetActive(false);
        info.SetActive(true);
    }
    public void BackMenu()
    {
        Debug.Log("Back to Main Menu");
        main.SetActive(true);
        levels.SetActive(false);
        info.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
