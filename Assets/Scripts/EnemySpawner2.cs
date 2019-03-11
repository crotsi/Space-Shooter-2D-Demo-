using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject enemy;
    public
    float maxSpawnRateInSeconds = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min.y = min.y + 2.4f;
        max.y = max.y - 1f;
        GameObject anEnemy = (GameObject)Instantiate(enemy);
        anEnemy.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));

        ScheduleNextEnemySpawn();
    }
    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(6f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 6f;

        Invoke("SpawnEnemy", spawnInSeconds);
    }
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
    public void ScheduleEnemySpawner()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
