using UnityEngine;

public class Infinity : MonoBehaviour
{

    public GameObject player;
    public GameObject cam;
    public GameObject cube;
    public GameObject red;
    private float spawnAhead;
    public Vector2 spawnOffsetY;
    public Vector2 spawnOffsetX;
    private float lastSpawnX = 0f;
    void Start()
    {
        cam = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        spawnAhead = Random.Range(spawnOffsetX.x, spawnOffsetX.y);
        float playerX = player.transform.position.x;
        while (lastSpawnX < playerX + spawnAhead)
        {
            float yOffset = Random.Range(spawnOffsetY.x, spawnOffsetY.y);
            Vector2 spawnPos = new Vector2(lastSpawnX + spawnAhead, player.transform.position.y + yOffset);
            Instantiate(cube, spawnPos, Quaternion.identity);
            lastSpawnX += spawnAhead;
        }
        red.transform.position = new Vector3(cam.transform.position.x, red.transform.position.y, red.transform.position.z);
    }
}