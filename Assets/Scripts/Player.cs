using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool gunLoaded = true;
    bool powerShootEnabled;
    float horizontal;
    float vertical;
    Vector3 moveDirection;
    Vector2 facingDirection;
    CameraController camController;

    public float speed = 5;
    
    [SerializeField] int health = 10;
    [SerializeField] bool invulnerable;
    [SerializeField] float blinkRate = 1;
    [SerializeField] float invulnerableTime = 3;
    [SerializeField] float fireRate = 1;
    [SerializeField] Transform aim;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] Camera cam;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.UpdateUIHealth(health);
        }
    }

    private void Start()
    {
        camController = FindObjectOfType<CameraController>();
        UIManager.Instance.UpdateUIHealth(health);
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection.x = horizontal;
        moveDirection.y = vertical;

        transform.position += moveDirection * Time.deltaTime * speed;

        //Aim Direction
        facingDirection = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized ;

        //print("Facing Direction = mousePosition - playerPosition" + facingDirection + " " + cam.ScreenToWorldPoint(Input.mousePosition) + "-" + transform.position);
        //print("Aim Position = playerPosition - facingDirection" + aim.position + "=" + transform.position + "+" + (Vector3)facingDirection.normalized);

        if (Input.GetMouseButton(0) && gunLoaded) {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);
            if (powerShootEnabled)
            {
                bulletClone.GetComponent<Bullet>().powerShoot = true;
            }
            StartCoroutine(ReloadGun());
        }

        anim.SetFloat("Speed", moveDirection.magnitude);

        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        } else if (aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }

    public void TakeDamage()
    {
        if (invulnerable)
            return;

        Health--;
        invulnerable = true;
        fireRate = 1;
        powerShootEnabled = false;
        camController.Shake();
        StartCoroutine(MakeVulnerableAgain());
        if (Health <= 0)
        {
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
        }
    }

    IEnumerator MakeVulnerableAgain()
    {
        StartCoroutine(BlinkRountine());
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    IEnumerator BlinkRountine()
    {
        int blinkTime = 10;
        while (blinkTime > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkTime * blinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkTime * blinkRate);
            blinkTime--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            switch (collision.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    powerShootEnabled = true;
                    break;
            }
            Destroy(collision.gameObject, 0.1f);
        }
    }
}
