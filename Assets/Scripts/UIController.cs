using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIController : MonoBehaviour
{
    [SerializeField]
    public PostProcessVolume volume;
    public GameObject main;
    public GameObject levels;  
    public GameObject settings;
    void Awake()
    {
    }

    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Player");
        volume=FindFirstObjectByType<PostProcessVolume>();      
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void LevelsMenu()
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
    public void L1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void L2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    public void L3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
    public void L4()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }
    public void Fast()
    {
        volume.profile.GetSetting<Bloom>().fastMode.value = true;
        volume.profile.GetSetting<ChromaticAberration>().fastMode.value = true;
    }
    public void Qual()
    {
        volume.profile.GetSetting<Bloom>().fastMode.value = false;
        volume.profile.GetSetting<ChromaticAberration>().fastMode.value = false;
    }
}
