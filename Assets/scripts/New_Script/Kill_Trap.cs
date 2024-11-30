using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill_Trap : MonoBehaviour
{
    [SerializeField] float delay = 0.2f;  // Tiempo de espera antes de realizar el respawn
    [SerializeField] Checkpoint checkpointScript;  // Referencia al script de Checkpoint
    [SerializeField] GameObject playerPrefab;  // Prefab del jugador para respawnear, si es necesario

    private void OnTriggerEnter(Collider other)  // Usamos Collider en lugar de Collider2D
    {
        // Verificamos si el objeto que entra en la zona de muerte es el jugador
        if (other.CompareTag("Player"))
        {
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