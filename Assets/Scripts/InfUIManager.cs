using UnityEngine;
using TMPro;

public class InfUIManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public Rigidbody2D rb;
    public PlayerController pc;
    public TMP_Text timerText;
    public TMP_Text highScoreText;
    public TMP_Text speedText;
    public TMP_Text startText;

    void Start()
    {
        player=GameObject.FindWithTag("Player");
        canvas=GameObject.Find("Canvas");
        timerText = canvas.transform.Find("TimerText").GetComponent<TMP_Text>();
        highScoreText = canvas.transform.Find("HighScoreText").GetComponent<TMP_Text>();
        speedText = canvas.transform.Find("SpeedText").GetComponent<TMP_Text>();
        startText = canvas.transform.Find("StartText").GetComponent<TMP_Text>();
        startText.gameObject.SetActive(false);
        rb=player.GetComponent<Rigidbody2D>();
        pc=player.GetComponent<PlayerController>();
        if(PlayerPrefs.HasKey("HighScoreInfinity"))
        {
            float highScore = PlayerPrefs.GetFloat("HighScoreInfinity");
            highScoreText.text = highScore.ToString("F1")+" m";
        }
        else
        {
            highScoreText.text = "--- m";
        }   
    }
    void Update()
    {
       
    }
    void FixedUpdate()
    {
        timerText.text = player.transform.position.x.ToString("F1") + " m";
        speedText.text = rb.linearVelocity.magnitude.ToString("F1") + " Unit/Sec";
    }
}
