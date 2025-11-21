using UnityEngine;
using Unity.Mathematics;

public class Trophy : MonoBehaviour
{
    public bool rotate = false;
    public float oscillationHeight = 0.5f;
    public float oscillationSpeed = 2f;
    public float rotationSpeed = 1f;
    private Vector3 startPosition;
    private GameManager gm;
    private PlayerController pc;
    private GameObject player;
    
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        startPosition = transform.position;
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
        PlayerPrefs.SetFloat("HighScore", math.min(gm.timer, PlayerPrefs.GetFloat("HighScore",Mathf.Infinity)));
        int current = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int next = current + 1;
        if (next > UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1) next = 1;        
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(next);
        pc.Release();         
        }
    public void Lose()
    {
        player.transform.position = Vector3.zero;   
    }
}
