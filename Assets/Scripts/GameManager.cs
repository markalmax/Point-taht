using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float timer = 0f;
    public float startWait = 3f;
    public GameObject canvas;
    public GameObject player;
    public Rigidbody2D rb;
    public PlayerController pc;
    public TMP_Text timerText;
    public TMP_Text highScoreText;
    public TMP_Text speedText;
    public TMP_Text startText;
    public bool startTimer = true;
    private float startTime;
    void Start()
    {
        if (PlayerPrefs.HasKey("DisableTimer") && PlayerPrefs.GetInt("DisableTimer") == 1)
        {
            startTimer = false;
            PlayerPrefs.DeleteKey("DisableTimer");
        }
        player=GameObject.FindWithTag("Player");
        canvas=GameObject.Find("Canvas");
        timerText = canvas.transform.Find("TimerText").GetComponent<TMP_Text>();
        highScoreText = canvas.transform.Find("HighScoreText").GetComponent<TMP_Text>();
        speedText = canvas.transform.Find("SpeedText").GetComponent<TMP_Text>();
        startText = canvas.transform.Find("StartText").GetComponent<TMP_Text>();
        startTime = startWait; 
        rb=player.GetComponent<Rigidbody2D>();
        pc=player.GetComponent<PlayerController>();
        if (PlayerPrefs.HasKey("HighScore"+SceneManager.GetActiveScene().buildIndex))
        {
            float highScore = PlayerPrefs.GetFloat("HighScore"+SceneManager.GetActiveScene().buildIndex);
            //Debug.Log(highScore);
            TimeSpan time = TimeSpan.FromSeconds(highScore);
            highScoreText.text = time.ToString(@"mm\:ss\:fff");
            //Debug.Log(time.ToString(@"mm\:ss\:fff"));
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
        if(!startTimer)
        {
            startText.gameObject.SetActive(false);
            startTime = 0f;
        }
          if (startTime <= 0f)
        {
            pc.canMove = true;
            startText.text = "Go!";
        }
        else
        {
            startTime -= Time.deltaTime;
            string timeDisplay = startTime.ToString("F0");
            if (timeDisplay == "0")timeDisplay = "Go!";
            startText.text = timeDisplay;
        }  
        if(pc.canMove)timer += Time.deltaTime;
        speedText.text = rb.linearVelocity.magnitude.ToString("F1") + " Unit/Sec";
        TimeSpan time = TimeSpan.FromSeconds(timer);
        if (time.TotalSeconds >= 1)
        {
            startText.gameObject.SetActive(false);
        }
        timerText.text = time.ToString(@"mm\:ss\:fff");
    }
}
