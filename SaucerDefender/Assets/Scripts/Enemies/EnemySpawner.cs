using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyModels;
    [SerializeField] private List<GameObject> futureEnemies;
    [SerializeField] private Vector2 spawnInterval;
    [SerializeField] private ScriptableSignal OnLeave;
    [SerializeField] private ScriptableSignal OnFinalWave;

    private Vector2 currentInterval;
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> enemyPool = new List<GameObject>();
    private Timer timer;


    private void Start()
    {
        enemies = new List<GameObject>(enemyModels);
        currentInterval = spawnInterval;

        var researchManager = FractaMaster.GetManager<ResearchManager>();
        researchManager.OnResearchCompleted += StopSpawning;
        researchManager.OnResearchStarted += StartSpawning;
        OnLeave.Register(ResetPool);
        OnFinalWave.Register(StartSpawning);
        OnLeave.Register(KillAll);

        timer = GetComponent<Timer>();
        timer.OnTimeOut += SpawnEnemy;
        var rdm = Random.Range(spawnInterval.x, spawnInterval.y);
        timer.SetTime(rdm);
    }

    private void KillAll()
    {
        StopSpawning();
        var enemies = FindObjectsOfType<EnemyAttackController>();

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<HealthModule>().CurrentHealth -= 999;
        }
    }

    private void StartSpawning()
    {
        SpawnEnemy();
        timer.StartTimer();
    }

    private void StopSpawning()
    {
        timer.StopTimer();

        currentInterval /= 1.33f;
        enemyModels.AddRange(futureEnemies);
    }

    private void ResetPool()
    {
        enemies = new List<GameObject>(enemyModels);
        currentInterval = spawnInterval;
    }

    private void SpawnEnemy()
    {
        var model = enemies[Random.Range(0, enemies.Count)];

        GameObject enemy;
        enemy = enemyPool.Find(x => x.name == model.name);

        if(enemy == null)
        {
            enemy = Instantiate(model, Vector3.zero, Quaternion.identity);
            enemy.GetComponent<HealthModule>().OnDeath += ReturnToPool;
            enemy.name = model.name;
        } else
        {
            enemyPool.Remove(enemy);
        }

        enemy.GetComponent<AiMovementController>().Initialize();
        var rdm = Random.Range(currentInterval.x, currentInterval.y);
        timer.SetTime(rdm);
        timer.StartTimer();
    }

    private void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.position = Vector3.zero;
        enemyPool.Add(enemy);
    }
}
 