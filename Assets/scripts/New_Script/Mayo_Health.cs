using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayo_Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBar healthBar;

    private bool isDead = false;

    private Animator animator;

    public MonoBehaviour scriptToDeactivate;

    public GameObject objectToActivate;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Boss is dead");

        // Desactivar Apply Root Motion
        if (animator != null)
        {
            animator.applyRootMotion = false;
            animator.SetTrigger("Die"); // Asegúrate de tener un trigger "Die" en el Animator
        }

        if (scriptToDeactivate != null)
        {
            scriptToDeactivate.enabled = false;
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log($"{objectToActivate.name} activado.");
        }

        // Aquí puedes añadir más lógica si es necesario, como desactivar el movimiento
        // o asegurarte de que el enemigo queda inmóvil durante la animación de muerte.
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
