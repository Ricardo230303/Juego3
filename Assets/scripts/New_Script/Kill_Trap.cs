using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill_Trap : MonoBehaviour
{
    [SerializeField] float delay = 0.2f;  // Tiempo de espera antes de realizar el respawn
    [SerializeField] Checkpoint checkpointScript;  // Referencia al script de Checkpoint
    [SerializeField] GameObject playerPrefab;  // Prefab del jugador para respawnear, si es necesario

    [SerializeField] AudioClip deathSound;  // Sonido de la trampa al colisionar
    private AudioSource audioSource;  //

    private void Start()
    {
        // Obtener el componente AudioSource en el mismo GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en el GameObject de la trampa.");
        }
    }

    private void OnTriggerEnter(Collider other)  // Usamos Collider en lugar de Collider2D
    {
        // Verificamos si el objeto que entra en la zona de muerte es el jugador
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);  // Reproducir el sonido de la muerte
            }
            // Desactivamos al jugador temporalmente para que no se vea durante el respawn
            other.gameObject.SetActive(false);

            // Llamamos al método que respawnea al jugador en el último checkpoint
            checkpointScript.RespawnAtCheckpoint(other.gameObject);

            // Esperamos el tiempo de espera (delay) antes de reactivar al jugador
            StartCoroutine(WaitAndRespawn(other.gameObject));
        }
    }

    // Corutina que maneja el retraso antes de reactivar al jugador
    IEnumerator WaitAndRespawn(GameObject player)
    {
        // Esperamos el tiempo del delay
        yield return new WaitForSeconds(delay);

        // Reactivamos al jugador en la posición correcta (en el checkpoint)
        player.SetActive(true);

        // Opcional: Aquí puedes añadir efectos visuales o sonidos si es necesario

        // Después de que el jugador haya sido reactivado en el checkpoint, se continúa sin recargar la escena.
    }
}