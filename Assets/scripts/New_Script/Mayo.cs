using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayo : MonoBehaviour
{
    [Header("Mortar Settings")]
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform[] firePoints; // Lista de puntos de disparo
    public float launchForce = 10f; // Fuerza inicial del disparo
    public float fireCooldown = 3f; // Tiempo de espera entre disparos
    public float projectileLifetime = 5f;

    [Header("Animation Settings")]
    public string fireAnimationTrigger = "Firing"; // Trigger para la animaci�n de disparo
    public string idleAnimationTrigger = "Idle"; // Trigger para la animaci�n de idle

    private Animator animator;
    private float nextFireTime = 0f;

    private void Start()
    {
        // Obtener el Animator del enemigo
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No se encontr� un Animator en el enemigo.");
        }

        // Asegurarse de que comience en Idle
        if (animator != null && !string.IsNullOrEmpty(idleAnimationTrigger))
        {
            animator.SetTrigger(idleAnimationTrigger);
        }
    }

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            StartCoroutine(FireSequence());
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private System.Collections.IEnumerator FireSequence()
    {
        // Cambiar a la animaci�n de disparo
        if (animator != null && !string.IsNullOrEmpty(fireAnimationTrigger))
        {
            animator.SetTrigger(fireAnimationTrigger);
        }

        // Esperar el tiempo de la animaci�n de disparo
        yield return new WaitForSeconds(0.5f); // Ajustar seg�n la duraci�n de tu animaci�n

        // Disparar desde todos los firePoints
        FireMortar();

        // Volver a la animaci�n de Idle
        if (animator != null && !string.IsNullOrEmpty(idleAnimationTrigger))
        {
            animator.SetTrigger(idleAnimationTrigger);
        }
    }

    private void FireMortar()
    {
        foreach (Transform firePoint in firePoints)
        {
            if (firePoint == null) continue;

            // Instanciar el proyectil
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Calcula la direcci�n de lanzamiento con un �ngulo hacia arriba
                Vector3 launchDirection = firePoint.forward + Vector3.up * 0.5f;

                // Dibuja un rayo para visualizar la direcci�n de lanzamiento
                Debug.DrawRay(firePoint.position, launchDirection.normalized * 2, Color.red, 2f);

                // Aplicar la fuerza al proyectil
                rb.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);

                // Aseg�rate de que la gravedad est� activada
                rb.useGravity = true;
            }
            Destroy(projectile, projectileLifetime);
        }
    }
}