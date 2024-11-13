using UnityEngine;
public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // El proyectil que se instanciar�
    [SerializeField] Transform firePoint; // El punto desde donde se dispara el proyectil
    [SerializeField] float projectileSpeed = 20f; // Velocidad del proyectil
    [SerializeField] float fireRate = 0.5f; // Tiempo entre disparos
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1f;
    [SerializeField] SpriteRenderer sr; // Referencia al SpriteRenderer del jugador


    [SerializeField] float aimingOffsetZ = 1f;


    void Update()
    {
        // Disparo cuando se presiona el bot�n de disparo y ha pasado el tiempo de espera
        if (Input.GetKey("j") && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // Cambiar la posici�n del firePoint al mantener presionada la tecla "K"
        if (Input.GetKey("k"))
        {
            UpdateFirePointPosition(true); // Mover el firePoint a la nueva posici�n de apuntado
        }
        else
        {
            UpdateFirePointPosition(false); // Volver a la posici�n original del firePoint
        }

        // Rotar el firePoint en funci�n de la direcci�n del jugador
        UpdateFirePointRotation();
    }

    void Shoot()
    {
        // Crear un nuevo proyectil en la posici�n y rotaci�n del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Obtener el componente Rigidbody del proyectil y aplicar la fuerza
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Direcci�n del disparo basada en la orientaci�n del firePoint
        Vector3 shootDirection = firePoint.forward;

        // Asegurarse de que el proyectil se dispare en la direcci�n correcta
        rb.velocity = shootDirection.normalized * projectileSpeed;

        // Destruir el proyectil despu�s de un tiempo
        Destroy(projectile, bulletLife);
    }



    // Actualiza la rotaci�n del firePoint seg�n la direcci�n del jugador
    void UpdateFirePointRotation()
    {
        // Si el jugador se mueve a la izquierda o derecha, invertir el firePoint
        if (sr != null)
        {
            if (sr.flipX)  // Si el sprite est� invertido (mirando a la izquierda)
            {
                firePoint.rotation = Quaternion.Euler(0, -90, 0); // Rotar el firePoint 180 grados
            }
            else  // Si el sprite est� mirando a la derecha
            {
                firePoint.rotation = Quaternion.Euler(0, 90, 0); // Mantener la rotaci�n original
            }
        }
    }

    void UpdateFirePointPosition(bool isAiming)
    {
        // Desplazar el firePoint en el eje Z cuando el jugador est� apuntando
        if (isAiming)
        {
            firePoint.localPosition = new Vector3(firePoint.localPosition.x, firePoint.localPosition.y, aimingOffsetZ);
        }
        else
        {
            firePoint.localPosition = new Vector3(firePoint.localPosition.x, firePoint.localPosition.y, 0); // Volver a la posici�n original
        }
    }
}