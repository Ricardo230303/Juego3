using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Erratic : MonoBehaviour
{
    public float roamRadius = 10f; // Radio de movimiento aleatorio
    public float roamInterval = 3f; // Intervalo entre movimientos
    private NavMeshAgent agent;
    private Vector3 destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        InvokeRepeating("SetRandomDestination", 0f, roamInterval);
    }

        void Update()
    {
        // Mantener la rotación del objeto igual a la rotación original (solo sobre el eje Y)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void SetRandomDestination()
    {
        // Generar una posición aleatoria dentro del radio
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;

        // Asegurarse de que la posición esté en el NavMesh
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas);
        destination = hit.position;

        // Mover al enemigo hacia la nueva posición
        agent.SetDestination(destination);
    }
}