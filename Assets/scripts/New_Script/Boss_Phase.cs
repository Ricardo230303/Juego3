using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase : MonoBehaviour
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

    private enum BossHealthState
    {
        HighHealth,
        LowHealth
    }
    private BossHealthState currentHealthState;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        currentHealthState = BossHealthState.HighHealth;
    }

    private void Update()
    {
        if (isDead) return;

        FollowPlayer();
        FireAtPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 direction = player.position - bulletSpawnPoint.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        bulletSpawnPoint.rotation = Quaternion.Slerp(bulletSpawnPoint.rotation, targetRotation, Time.deltaTime * 5f); // Rotación suave
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

        if (currentHealth > 0.5f * maxHealth)
        {
            if (currentHealthState != BossHealthState.HighHealth)
            {
                currentHealthState = BossHealthState.HighHealth;
                if (animator != null)
                {
                    animator.SetTrigger("HighHealth");
                }
            }
            else
            {
                if (currentHealthState != BossHealthState.LowHealth)
                {
                    currentHealthState = BossHealthState.LowHealth;
                    if (animator != null)
                    {
                        animator.SetTrigger("LowHealth");
                    }
                }
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
        {
            isDead = true;
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