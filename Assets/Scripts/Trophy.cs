using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class Trophy : MonoBehaviour
{
    public bool rotate = false;
    public float oscillationHeight = 0.5f;
    public float oscillationSpeed = 2f;
    public float rotationSpeed = 1f;
    private Vector3 startPosition;
    public GameManager gm;
    public GameObject spawn;
    public PlayerController pc;
    public GameObject player;
    
    void Start()
    {
        startPosition = transform.position;
        spawn = GameObject.FindWithTag("Spawn");
        gm = FindFirstObjectByType<GameManager>();
        player = GameObject.FindWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if(rotate)transform.Rotate(0, rotationSpeed, 0);        
        float newY = startPosition.y + Mathf.Sin(Time.time * oscillationSpeed) * oscillationHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    public void Win()
    {
        PlayerPrefs.SetFloat("HighScore"+SceneManager.GetActiveScene().buildIndex, math.min(gm.timer, PlayerPrefs.GetFloat("HighScore",Mathf.Infinity)));   
        PlayerPrefs.SetInt("DisableTimer", 1);
        if(SceneManager.GetActiveScene().buildIndex + 1 == 5)
        {
            SceneManager.LoadSceneAsync(0);
            pc.Release(); 
            return;
        }
       SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        pc.Release();         
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Win();
        }
    }
}
