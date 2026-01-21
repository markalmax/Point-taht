using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    public PostProcessVolume volume;
    public GameObject main;
    public GameObject mainHB;
    public GameObject levels;  
    public GameObject settings;
    public PlayerController pc;
    public TMP_Text[] levelTimes;
    public GameObject[] spawnPoints;    
    void Start()
    {
        pc = FindFirstObjectByType<PlayerController>();
        volume=FindFirstObjectByType<PostProcessVolume>();    
        if(PlayerPrefs.GetInt("Mode",0)==1)
        {
            Qual();
        }
        else
        {
            Fast();
        }
        for(int i=0;i<levelTimes.Length;i++)
        {
            if (PlayerPrefs.HasKey("HighScore"+(i+1)))
            {
                float levelTime = PlayerPrefs.GetFloat("HighScore"+(i+1));
                TimeSpan time = TimeSpan.FromSeconds(levelTime);
                levelTimes[i].text = time.ToString(@"mm\:ss\:fff");
            }
            else
            {
                levelTimes[i].text = "--:--:---";
            }
        }
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
    public void LevelsMenu()
    {
        main.SetActive(false);
        mainHB.SetActive(false);
        levels.SetActive(true);
    }
    public void SettingsMenu()
    {
        main.SetActive(false);
        mainHB.SetActive(false);
        settings.SetActive(true);
    }
    public void BackMenu()
    {
        main.SetActive(true);
        mainHB.SetActive(true);
        levels.SetActive(false);
        settings.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void L1()
    {   
        pc.SpawnTP(spawnPoints[0].transform.position);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
    public void L2()
    {
        pc.SpawnTP(spawnPoints[1].transform.position);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
    }
    public void L3()
    {
        pc.SpawnTP(spawnPoints[2].transform.position);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3); 
    }
    public void L4()
    {
        pc.SpawnTP(spawnPoints[3].transform.position);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4);
    }
    public void Fast()
    {
        volume.profile.GetSetting<Bloom>().fastMode.value = true;
        volume.profile.GetSetting<ChromaticAberration>().fastMode.value = true;
        QualitySettings.SetQualityLevel(0);
        PlayerPrefs.SetInt("Mode", 0);
    }
    public void Qual()
    {
        volume.profile.GetSetting<Bloom>().fastMode.value = false;
        volume.profile.GetSetting<ChromaticAberration>().fastMode.value = false;
        QualitySettings.SetQualityLevel(6);
        PlayerPrefs.SetInt("Mode", 1);
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
    
}
