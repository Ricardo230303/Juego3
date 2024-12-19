using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextSceneName; // Nombre de la escena a cargar
    public GameObject objectToActivate; // Objeto que se encenderá al colisionar
    public float delayBeforeSceneChange = 1.5f; // Tiempo de espera antes de cambiar de escena

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ActivateObjectAndChangeScene());
        }
    }

    private IEnumerator ActivateObjectAndChangeScene()
    {
        // Activar el objeto si está asignado
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // Esperar el tiempo especificado
        yield return new WaitForSeconds(delayBeforeSceneChange);

        // Cargar la siguiente escena
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName) && SceneManager.GetSceneByName(nextSceneName) != null)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No se pudo cargar la escena siguiente. Asegúrate de proporcionar un nombre de escena válido en el Inspector.");
        }
    }
}