using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera; // La c�mara principal de Cinemachine
    public CinemachineVirtualCamera newCamera;  // La nueva c�mara de Cinemachine a la que queremos cambiar

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el que entra en el trigger es el jugador (puedes usar una etiqueta para esto)
        if (other.CompareTag("Player"))
        {
            // Desactivamos la c�mara principal
            if (mainCamera != null)
                mainCamera.gameObject.SetActive(false);

            // Activamos la nueva c�mara
            if (newCamera != null)
                newCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir del trigger, volvemos a la c�mara principal
        if (other.CompareTag("Player"))
        {
            if (mainCamera != null)
                mainCamera.gameObject.SetActive(true);

            if (newCamera != null)
                newCamera.gameObject.SetActive(false);
        }
    }
}
