using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject main;
    public GameObject levels;  
    public GameObject info;
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void levelsMenu()
    {
        main.SetActive(false);
        levels.SetActive(true);
    }
    public void InfoMenu()
    {
        main.SetActive(false);
        info.SetActive(true);
    }
    public void BackMenu()
    {
        main.SetActive(true);
        levels.SetActive(false);
        info.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
