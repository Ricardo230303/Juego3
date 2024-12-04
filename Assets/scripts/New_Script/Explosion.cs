using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Referencia al componente AudioSource
    private AudioSource audioSource;

    // Sonido que se reproducir� al colisionar con el "Ground"
    public AudioClip collisionSound;

    void Start()
    {
        // Obtenemos el componente AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontr� un AudioSource en el GameObject.");
        }
    }

    // M�todo que se ejecuta cuando entra en un Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto con el que colisionamos tiene el layer "Ground"
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Reproducir el sonido si el AudioSource est� disponible
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }
}