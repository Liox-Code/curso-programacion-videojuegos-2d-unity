using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool powerShoot;

    [SerializeField] float speed = 5;
    [SerializeField] int bulletLiveTime = 1;
    [SerializeField] int health = 3;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
        Destroy(gameObject, bulletLiveTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().takeDamage();

            if (!powerShoot)
            {
                Destroy(gameObject);
            }

            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
