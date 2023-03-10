using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] int speed = 3;
    [SerializeField] int health = 1;
    [SerializeField] int scorePoints = 100;
    [SerializeField] AudioClip impactClip;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
    }

    public void takeDamage()
    {
        health--;
        AudioSource.PlayClipAtPoint(impactClip, transform.position);

        if (health <= 0)
        {
            GameManager.Instance.Score += scorePoints;
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();
        }
    }
}
