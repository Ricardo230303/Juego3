using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f; 
    public float fireRate = 1f;

    private Transform player; 
    private bool isDead = false;
    private float nextFireTime = 0f; 
    private Animator animator; 

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        if (isDead) return;

        FollowPlayer();
        FireAtPlayer(); 

        /*
        if (animator != null)
        {
            animator.SetBool("IsMoving", true); 
        }
        */
    }

    private void FollowPlayer()
    {
        Vector3 direction = player.position - bulletSpawnPoint.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        bulletSpawnPoint.rotation = Quaternion.Slerp(bulletSpawnPoint.rotation, targetRotation, Time.deltaTime * 5f); // Rotación más suave

    }

    private void FireAtPlayer()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        float healthRatio = currentHealth / maxHealth;

        if (healthRatio > 0.5f)
        {
            if (animator != null)
            {
                animator.SetTrigger("HighHealth");
            }
        }
        else if (healthRatio <= 0.5f)
        {
            if (animator != null)
            {
                animator.SetTrigger("LowHealth");
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        /*
        if (animator != null)
        {
            animator.SetTrigger("Die"); 
        }
        */
        Destroy(gameObject);
    }

  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet")) 
        {
            float damage = collision.gameObject.GetComponent<Bullet>().damage;
            TakeDamage(damage); 
            Destroy(collision.gameObject); 
        }
    }
}


