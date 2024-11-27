using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Phase : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject bulletPrefab;
    public Transform[] bulletSpawnPoints;  // Array de puntos de disparo
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public float bulletLife = 2f;

    public HealthBar healthBar;


    private bool isDead = false;
    private float[] nextFireTime;

    public string nextSceneName;


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
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        animator = GetComponent<Animator>();

        // Inicializar el estado de salud
        currentHealthState = BossHealthState.HighHealth;

        // Inicializar el array de tiempos de disparo para cada firePoint
        nextFireTime = new float[bulletSpawnPoints.Length];
        for (int i = 0; i < nextFireTime.Length; i++)
        {
            nextFireTime[i] = Time.time + (i * 0.2f);
        }
    }
    private void Update()
    {
        if (isDead) return;

        FireAtPlayer();
    }

    private void FireAtPlayer()
    {
        for (int i = 0; i < bulletSpawnPoints.Length; i++)
        {
            // Solo disparar si ha pasado el tiempo necesario para ese firePoint
            if (Time.time >= nextFireTime[i])
            {
                nextFireTime[i] = Time.time + 1f / fireRate;  // Asignar el próximo tiempo de disparo para este firePoint

                // Disparar desde el punto de disparo
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoints[i].position, bulletSpawnPoints[i].rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bulletSpawnPoints[i].forward * bulletSpeed;  // Disparar la bala en la dirección del firePoint
                }

                Destroy(bullet, bulletLife);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        Debug.Log("Current Health: " + currentHealth);

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

        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;

        // Si tienes un Animator, activamos la animación de muerte
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Comienza una Coroutine para esperar antes de cargar la siguiente escena
        StartCoroutine(WaitAndLoadScene(3f)); // Espera 3 segundos (ajusta el tiempo según la duración de la animación)
    }

    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        // Espera el tiempo dado antes de continuar
        yield return new WaitForSeconds(waitTime);

        // Ahora cargamos la siguiente escena
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName) && SceneManager.GetSceneByName(nextSceneName) != null)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No se pudo cargar la escena siguiente. Asegúrate de proporcionar un nombre de escena válido en el Inspector.");
        }
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