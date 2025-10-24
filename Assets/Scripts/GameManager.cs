using System;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public float timer = 0f;
    public TMP_Text timerText;
    public TMP_Text highScoreText;
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            float highScore = PlayerPrefs.GetFloat("HighScore");
            Debug.Log(highScore);
            TimeSpan time = TimeSpan.FromSeconds(highScore);
            highScoreText.text = "Best Time:" + time.ToString(@"mm\:ss\:fff");
            Debug.Log(time.ToString(@"mm\:ss\:fff"));
        }
        else
        {
            highScoreText.text = "Best Time:--:--:---";
        }
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = time.ToString(@"mm\:ss\:fff");
    }
}
