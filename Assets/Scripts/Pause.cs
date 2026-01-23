using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    public PlayerController pc;

    void Start()
    {
        pauseMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pc.canMove = true;
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pc.canMove = false;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        pc.Release();player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.transform.position = GameObject.FindWithTag("Spawn").transform.position;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        Destroy(player);
        SceneManager.LoadSceneAsync(0);
    }
}
