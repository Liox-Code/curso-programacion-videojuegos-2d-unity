using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Range(1, 10)] [SerializeField] float spawnRate = 1;
    [SerializeField] GameObject[] enemyPrefabs;
    
    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    // Update is called once per frame
    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / spawnRate);
            float random = Random.Range(0.0f,1.0f);
            if (random > GameManager.Instance.difficulty * 0.1)
            {
                Instantiate(enemyPrefabs[0]);
            }
            else
            {
                Instantiate(enemyPrefabs[1]);
            }
        }
    }
}
