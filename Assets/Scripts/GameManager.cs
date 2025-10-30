using System;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public float timer = 0f;
    public float startWait = 3f;
    public Rigidbody2D rb;
    public TMP_Text timerText;
    public TMP_Text highScoreText;
    public TMP_Text speedText;
    public TMP_Text startText;
    private bool flag;
    private float startTime;
    void Start()
    {
        startTime = startWait;
        rb=FindFirstObjectByType<Rigidbody2D>();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            float highScore = PlayerPrefs.GetFloat("HighScore");
            Debug.Log(highScore);
            TimeSpan time = TimeSpan.FromSeconds(highScore);
            highScoreText.text = time.ToString(@"mm\:ss\:fff");
            Debug.Log(time.ToString(@"mm\:ss\:fff"));
        }
        else
        {
            highScoreText.text = "--:--:---";
        }
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (startTime <= 0f)
        {
            flag = true;
            startText.text = "Go!";
        }
        else
        {
            startTime -= Time.deltaTime;
            string timeDisplay = startTime.ToString("F0");
            if (timeDisplay == "0")timeDisplay = "Go!";
            startText.text = timeDisplay;
        }
        if(flag)timer += Time.deltaTime;
        speedText.text = rb.linearVelocity.magnitude.ToString("F1") + " Unit/Sec";
        TimeSpan time = TimeSpan.FromSeconds(timer);
        if (time.TotalSeconds >= 1)
        {
            startText.gameObject.SetActive(false);
        }
        timerText.text = time.ToString(@"mm\:ss\:fff");
    }
}
