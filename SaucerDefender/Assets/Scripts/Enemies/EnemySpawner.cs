using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyModel;
    [SerializeField] private float spawnInterval;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private Timer timer;


    private void Start()
    {
        var researchManager = FractaMaster.GetManager<ResearchManager>();
        researchManager.OnResearchCompleted += StopSpawning;
        researchManager.OnResearchStarted += StartSpawning;

        timer = GetComponent<Timer>();
        timer.OnTimeOut += SpawnEnemy;
        timer.SetTime(spawnInterval);

        StartSpawning();
    }

    private void StartSpawning()
    {
        SpawnEnemy();
        timer.StartTimer();
    }

    private void StopSpawning()
    {
        SpawnEnemy();
        timer.StopTimer();
    }

    private void SpawnEnemy()
    {
        GameObject enemy;

        if(enemyPool.Count == 0)
        {
            enemy = Instantiate(enemyModel);
            enemy.GetComponent<HealthModule>().OnDeath += ReturnToPool;
        } else
        {
            enemy = enemyPool.Dequeue();
        }

        enemy.GetComponent<AiMovementController>().Initialize();
        timer.StartTimer();
    }

    private void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
