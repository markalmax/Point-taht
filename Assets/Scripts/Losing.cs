using UnityEngine;
using UnityEngine.SceneManagement;

public class Losing : MonoBehaviour
{
    public GameObject player;
    public GameObject spawn;
    public InfUIManager UI;
    public PlayerController pc;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawn = GameObject.FindWithTag("Spawn");
        pc = player.GetComponent<PlayerController>(); 
    }
    public void Lose()
    {
        player.transform.position = spawn.transform.position;
        pc.Release();   
    }
    public void Win()
    {
        PlayerPrefs.SetFloat("HighScoreInfinity", Mathf.Max(player.transform.position.x, PlayerPrefs.GetFloat("HighScoreInfinity", 0f)));
        Destroy(player);
        SceneManager.LoadSceneAsync(0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().buildIndex != 5)Lose();
            else Win();
        }
    }
}
