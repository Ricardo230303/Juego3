using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera; // La cámara principal de Cinemachine
    public CinemachineVirtualCamera newCamera;  // La nueva cámara de Cinemachine a la que queremos cambiar

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entra en el trigger es el jugador (puedes usar una etiqueta para esto)
        if (other.CompareTag("Player"))
        {
            // Desactivamos la cámara principal
            if (mainCamera != null)
                mainCamera.gameObject.SetActive(false);

            // Activamos la nueva cámara
            if (newCamera != null)
                newCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir del trigger, volvemos a la cámara principal
        if (other.CompareTag("Player"))
        {
            if (mainCamera != null)
                mainCamera.gameObject.SetActive(true);

            if (newCamera != null)
                newCamera.gameObject.SetActive(false);
        }
    }
}
