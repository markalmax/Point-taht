using UnityEngine;

public class Infinity : MonoBehaviour
{
    public GameObject blocks; 
    public GameObject player;
    public float minSpawnDistance = 23f;
    public float maxSpawnDistance = 30f;
    public float spawnDistanceThreshold = 25f;
    public float minYSpawnOffset = 0f;
    public float maxYSpawnOffset = 5f;


    private Vector2 lastSpawnPos;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        lastSpawnPos = player.transform.position;
    }
    void Update()
    {
        float distanceTraveled = Vector3.Distance(player.transform.position, lastSpawnPos);
        
        if (distanceTraveled >= spawnDistanceThreshold)
        {
            SpawnBlock();
            lastSpawnPos = player.transform.position;
        }
    }
    
    void SpawnBlock()
    {   
        float randomSpawnDist = Random.Range(minSpawnDistance, maxSpawnDistance);
        float randomYOffset = Random.Range(minYSpawnOffset, maxYSpawnOffset);
           
        Vector2 spawnPos = player.transform.position + player.transform.forward * randomSpawnDist + Vector3.up * randomYOffset;
        
        Instantiate(blocks, spawnPos, Quaternion.identity);
    }
}
