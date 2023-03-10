using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMiniLake : MonoBehaviour
{
    float originalSpeed;
    Player player;
    
    [SerializeField] float speedReductionRatio = 0.5f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            originalSpeed = player.GetComponent<Player>().speed;
            player.speed *= speedReductionRatio;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed = originalSpeed;
        }   
    }
}
