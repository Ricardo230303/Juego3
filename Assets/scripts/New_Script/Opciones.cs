using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Opciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject panelOpciones; // Referencia al panel de opciones

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void VolverAlMenuPrincipal()
    {
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(false); // Desactiva el panel de opciones
        }
    }
}