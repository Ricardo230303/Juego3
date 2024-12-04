using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isaac : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float moveSpeed = 5f;

    // Prefab del proyectil
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    // Variables para control de movimiento
    private Vector3 movement;

    // Variables para disparo continuo
    private float shootDelay = 0.2f; // Delay entre disparos (disparo continuo cada 0.1 segundos)
    private float lastShootTime = 0f;

    public SpriteRenderer sr;

    private Animator animator;

    public AudioClip shootSound;  // Sonido de disparo
    private AudioSource audioSource;  // Componente AudioSource

    void Start()
    {
        // Obtenemos la referencia al Animator
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en el GameObject.");
        }
    }
    void Update()
    {
        // Obtener entrada para el movimiento del jugador (A, D para X y W, S para Z)
        movement.x = Input.GetAxisRaw("Horizontal"); // A y D controlan X
        movement.z = Input.GetAxisRaw("Vertical");   // W y S controlan Z

        // Normalizar la dirección del movimiento para evitar movimiento diagonal más rápido
        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        // Mover al jugador
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Detectar entrada para disparo con flechas (disparo continuo mientras se mantenga la tecla presionada)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            TryFire(Vector3.forward); // Disparar hacia adelante (eje Z positivo)
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            TryFire(Vector3.back); // Disparar hacia atrás (eje Z negativo)
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            TryFire(Vector3.left); // Disparar hacia la izquierda (eje X negativo)
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            TryFire(Vector3.right); // Disparar hacia la derecha (eje X positivo)
        }
        // Flip el sprite cuando el jugador se mueve a la izquierda
        if (movement.x < 0)
        {
            sr.flipX = true;  // Voltear el sprite hacia la izquierda
        }
        else if (movement.x > 0)
        {
            sr.flipX = false; // Voltear el sprite hacia la derecha
        }

    }

    // Método para intentar disparar de forma continua
    void TryFire(Vector3 direction)
    {
        // Verificar si ha pasado el tiempo suficiente para disparar de nuevo
        if (Time.time - lastShootTime >= shootDelay)
        {
            lastShootTime = Time.time; // Registrar el tiempo del disparo actual
            Fire(direction); // Disparar el proyectil
        }
    }

    // Método para disparar un proyectil
    void Fire(Vector3 direction)
    {
        // Instanciar el proyectil
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Asegurarnos de que el proyectil se mueva solo en los ejes X y Z (sin rotación en Y)
        direction.y = 0; // Esto asegura que el proyectil no se mueva en el eje Y

        // Asignar la dirección y velocidad al proyectil
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);  // Reproducir el sonido de disparo
        }

        animator.SetTrigger("Shoot");
    }
}