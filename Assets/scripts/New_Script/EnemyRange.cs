using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRange : MonoBehaviour
{
    [SerializeField] UnityEvent onEnemyEnter;  // Evento cuando el enemigo entra al rango
    [SerializeField] UnityEvent onEnemyExit;   // Evento cuando el enemigo sale del rango
    [SerializeField] UnityEvent onEnemyStay;   // Evento cuando el enemigo permanece en el rango

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el rango tiene la etiqueta "Enemy"
        if (other.CompareTag("Enemy"))
        {
            onEnemyEnter?.Invoke();  // Invoca el evento de entrada
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del rango tiene la etiqueta "Enemy"
        if (other.CompareTag("Enemy"))
        {
            onEnemyExit?.Invoke();  // Invoca el evento de salida
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto que permanece en el rango tiene la etiqueta "Enemy"
        if (other.CompareTag("Enemy"))
        {
            onEnemyStay?.Invoke();  // Invoca el evento de permanencia
        }
    }
}