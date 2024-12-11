using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurting_Player : MonoBehaviour
{
    [SerializeField] GameOver gameOverScript;

    private bool isInvulnerable = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy_Bullet") || other.CompareTag("Enemy"))
        {
            if (!isInvulnerable) // Verificar si ya está invulnerable
            {
                Health_Heart.health--; // Reducir la vida
                if (Health_Heart.health <= 0)
                {
                    gameOverScript.ShowGameOverMenu(); // Mostrar pantalla de Game Over si la vida llega a 0
                }
                else
                {
                    StartCoroutine(GetHurt()); // Iniciar la corutina de invulnerabilidad
                }

                Debug.Log("Daño recibido");
            }
        }
    }
    IEnumerator GetHurt()
    {
        isInvulnerable = true;
        Physics.IgnoreLayerCollision(7, 8);
        yield return new WaitForSeconds(3);
        Physics.IgnoreLayerCollision(7, 8, false);
        isInvulnerable = false;
    }
}