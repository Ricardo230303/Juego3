
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palta : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBar healthBar;

    private bool isDead = false;

    private Animator animator;

    public MonoBehaviour scriptToDeactivate;

    public List<Palta> childElements;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        animator = GetComponent<Animator>();

        if (childElements == null || childElements.Count == 0)
        {
            childElements = new List<Palta>(GetComponentsInChildren<Palta>());
        }
    }

    private void Update()
    {
        if (isDead) return;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        float damagePerChild = damage / (childElements.Count + 1);

        foreach (Palta child in childElements)
        {
            child.TakeDamage(damagePerChild);
        }

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

        // Activar la animaci�n de muerte
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Aseg�rate de tener un trigger "Die" en el Animator
        }

        if (scriptToDeactivate != null)
        {
            scriptToDeactivate.enabled = false;
        }

        foreach (Palta child in childElements)
        {
            child.Die();  // Llamar a la muerte de los hijos
        }

        // Aqu� no desactivamos el objeto, solo ejecutamos la animaci�n
        // Si deseas que el objeto quede inm�vil, puedes hacerlo aqu� (opcional)
        // Ejemplo: agent.isStopped = true; si usas NavMeshAgent
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
