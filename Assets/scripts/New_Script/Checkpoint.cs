using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint; // El punto al que el jugador volver�

    // Esta funci�n se llama cuando el jugador entra en la zona del checkpoint
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que ha colisionado es el jugador
        if (other.CompareTag("Player"))
        {
            // Guardamos el punto de spawn del checkpoint
            PlayerPrefs.SetFloat("CheckpointX", spawnPoint.position.x);
            PlayerPrefs.SetFloat("CheckpointY", spawnPoint.position.y);
            PlayerPrefs.SetFloat("CheckpointZ", spawnPoint.position.z);

            // Opcionalmente, puedes mostrar un mensaje o efecto visual aqu�
            Debug.Log("Checkpoint activado!");
        }
    }

    // Esta funci�n permite al jugador volver al �ltimo checkpoint
    public void RespawnAtCheckpoint(GameObject player)
    {
        // Obt�n las coordenadas del �ltimo checkpoint guardado
        Vector3 checkpointPosition = new Vector3(
            PlayerPrefs.GetFloat("CheckpointX", player.transform.position.x),
            PlayerPrefs.GetFloat("CheckpointY", player.transform.position.y),
            PlayerPrefs.GetFloat("CheckpointZ", player.transform.position.z)
        );

        // Teletransporta al jugador al checkpoint guardado
        player.transform.position = checkpointPosition;

        // Opcionalmente, puedes hacer que el jugador tenga alguna animaci�n o efecto al reaparecer
    }
}