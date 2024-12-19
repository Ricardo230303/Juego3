using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody myBody; // Referencia al Rigidbody del enemigo
    public float speed = 5f; // Velocidad del enemigo
    public Transform playerTarget; // Referencia al transform del jugador

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        // Encuentra al jugador por su tag
        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (playerTarget == null) return; // Si no hay jugador, no hacer nada

        // Calcular la dirección hacia el jugador
        Vector3 direction = (playerTarget.position - transform.position).normalized;

        // Mantener la posición en el mismo plano (por ejemplo, ignorar Y si no es necesario)
        direction.y = 0;

        // Aplicar movimiento al Rigidbody
        myBody.velocity = direction * speed;
    }
}
