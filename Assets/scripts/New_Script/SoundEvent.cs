using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    public AudioClip soundClip;   // Clip de sonido
    private AudioSource audioSource;

    private void Start()
    {
        // Configurar AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // M�todo que se llama desde la animaci�n
    public void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogWarning("No hay clip de sonido asignado.");
        }
    }
}