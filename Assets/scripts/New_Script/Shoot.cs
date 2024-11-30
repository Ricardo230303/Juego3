using UnityEngine;
public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // El proyectil que se instanciará
    [SerializeField] Transform firePoint; // El punto desde donde se dispara el proyectil
    [SerializeField] float projectileSpeed = 20f; // Velocidad del proyectil
    [SerializeField] float fireRate = 0.5f; // Tiempo entre disparos
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1f;
    [SerializeField] SpriteRenderer sr; // Referencia al SpriteRenderer del jugador

    private Animator animator;


    // [SerializeField] float aimingOffsetZ = 1f;

    void Start()
    {
        // Obtenemos la referencia al Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Disparo cuando se presiona el botón de disparo y ha pasado el tiempo de espera
        if (Input.GetKey(KeyCode.LeftControl) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("Shoot_Up");
        }

        // Verifica si se presiona la tecla para disparar
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetTrigger("Shoot");
        }
        UpdateFirePointRotation();
    }

    void Shoot()
    {
        // Crear un nuevo proyectil en la posición y rotación del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Obtener el componente Rigidbody del proyectil y aplicar la fuerza
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Dirección del disparo basada en la orientación del firePoint
        Vector3 shootDirection = firePoint.forward;

        // Asegurarse de que el proyectil se dispare en la dirección correcta
        rb.velocity = shootDirection.normalized * projectileSpeed;

        // Destruir el proyectil después de un tiempo
        Destroy(projectile, bulletLife);
    }



    // Actualiza la rotación del firePoint según la dirección del jugador
    void UpdateFirePointRotation()
    {
        // Si el jugador se mueve a la izquierda o derecha, invertir el firePoint
        if (sr != null)
        {
            if (sr.flipX)  // Si el sprite está invertido (mirando a la izquierda)
            {
                firePoint.rotation = Quaternion.Euler(0, -90, 0); // Rotar el firePoint 180 grados
            }
            else  // Si el sprite está mirando a la derecha
            {
                firePoint.rotation = Quaternion.Euler(0, 90, 0); // Mantener la rotación original
            }
        }

        if (Input.GetKey("l"))
        {
            firePoint.rotation = Quaternion.Euler(-45, 90, 0);  // Rotar 45 grados sobre el eje X
        }
        else if  (Input.GetKey("i"))
        {
            firePoint.rotation = Quaternion.Euler(-90, 90, 0);  // Rotar 90 grados sobre el eje X
        }
        else if (Input.GetKey("j"))
        {
            firePoint.rotation = Quaternion.Euler(-135, 90, 0);
        }
    }
}
