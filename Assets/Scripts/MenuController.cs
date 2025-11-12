using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject main;
    public GameObject levels;  
    public GameObject settings;
    public void StartGame()
    {
        Debug.Log("Start Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void levelsMenu()
    {
        main.SetActive(false);
        levels.SetActive(true);
    }
    public void SettingsMenu()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }
    public void BackMenu()
    {
        main.SetActive(true);
        levels.SetActive(false);
        settings.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
