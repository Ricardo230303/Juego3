using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public AudioClip destructionSound;  // Clip de sonido que se reproducirá
    private AudioSource audioSource;    // AudioSource para reproducir el sonido

    private void Start()
    {
        // Añade un AudioSource automáticamente al objeto si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // No reproducir sonido al iniciar
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona tiene el tag "PlayerBullet"
        if (other.CompareTag("PlayerBullet"))
        {
            // Reproducir el sonido
            if (destructionSound != null)
            {
                audioSource.PlayOneShot(destructionSound);
            }
            else
            {
                Debug.LogWarning("No sound clip assigned for destruction.");
            }

            // Destruir el proyectil y el objeto después del sonido
            Destroy(other.gameObject); // Destruye la bala del personaje
            Destroy(gameObject, destructionSound.length); // Destruye este objeto después de que termine el sonido
        }
    }
}