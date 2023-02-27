using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPreb;
    private float spawnArea = 9.0f;
   private Vector3 spawnPoint;
    public int enemyCounter;
    public int spawnWave = 1;
    public GameObject powerUpPrebafs;
    private int levelDiffulicty = 0;
    void Start()
    {
        SpawnEnemyLoop(spawnWave);
        Instantiate(powerUpPrebafs, GenerateRandomPosition(), powerUpPrebafs.transform.rotation);
    }


    
    void Update()
    { 
        

        enemyCounter = FindObjectsOfType<Enemy>().Length;
        if(enemyCounter == 0)
        {
            spawnWave++;
            SpawnEnemyLoop(spawnWave);
            Instantiate(powerUpPrebafs, GenerateRandomPosition(), powerUpPrebafs.transform.rotation);
            levelDiffulicty++;
            SpawnBigEnemy();
        }

        
    }
    void SpawnEnemyLoop(int enemiesSpawn)
    {
        for (int i = 0; i < enemiesSpawn; i++)
        {

            Instantiate(enemyPreb[0], GenerateRandomPosition(), enemyPreb[0].transform.rotation);
            
           
        }
       

    }
    void SpawnBigEnemy()
    {
        if(levelDiffulicty > 4)
        {
            Instantiate(enemyPreb[1], GenerateRandomPosition(), enemyPreb[1].transform.rotation);
        }
    }
    private Vector3 GenerateRandomPosition()
    {

        float RandomX = Random.Range(-spawnArea, spawnArea);
        float RandomZ = Random.Range(-spawnArea, spawnArea);

        spawnPoint = new Vector3(RandomX, 0, RandomZ);
        return spawnPoint;
    }
}
