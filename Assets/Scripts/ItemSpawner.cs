using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] int checkPointSpawnDelay = 3;
    [SerializeField] int powerUpSpawnDelay = 3;
    [SerializeField] float spawnRadius = 5;
    [SerializeField] GameObject checkPointPrefab;
    [SerializeField] GameObject[] powerUpPrefabs;
    void Start()
    {
        StartCoroutine(SpawnCheckPointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnCheckPointRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkPointSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Instantiate(checkPointPrefab, randomPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomPowerUp], randomPosition, Quaternion.identity);
        }
    }

}
