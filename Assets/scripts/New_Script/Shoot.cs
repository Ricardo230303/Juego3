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

    public AudioClip shootSound;  // Sonido de disparo
    private AudioSource audioSource;  // Componente AudioSource


    // [SerializeField] float aimingOffsetZ = 1f;

    void Start()
    {
        // Obtenemos la referencia al Animator
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en el GameObject.");
        }
    }

    void Update()
    {
        // Verifica si se mantiene presionada alguna de las teclas de dirección para disparar
        if (Time.time > nextFireTime)
        {
            if (Input.GetKey(KeyCode.UpArrow))  // Flecha arriba
            {
                animator.SetTrigger("Shoot_Up");
                Shoot(Vector3.up);
                nextFireTime = Time.time + fireRate;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))  // Flecha izquierda
            {
                animator.SetTrigger("Shoot");
                Shoot(Vector3.left);
                nextFireTime = Time.time + fireRate;
            }
            else if (Input.GetKey(KeyCode.RightArrow))  // Flecha derecha
            {
                animator.SetTrigger("Shoot");
                Shoot(Vector3.right);
                nextFireTime = Time.time + fireRate;
            }
        }

        UpdateFirePointRotation();
    }

    void Shoot(Vector3 direction)
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);  // Reproducir el sonido de disparo
        }
        // Crear un nuevo proyectil en la posición y rotación del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Obtener el componente Rigidbody del proyectil y aplicar la fuerza
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Asegurarse de que el proyectil se dispare en la dirección correcta
        rb.velocity = direction.normalized * projectileSpeed;

        // Destruir el proyectil después de un tiempo
        Destroy(projectile, bulletLife);
    }

    // Actualiza la rotación del firePoint según la dirección del jugador
    void UpdateFirePointRotation()
    {
        if (sr != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                firePoint.rotation = Quaternion.Euler(0, -90, 0); // Rotar hacia la izquierda
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                firePoint.rotation = Quaternion.Euler(0, 90, 0); // Rotar hacia la derecha
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                firePoint.rotation = Quaternion.Euler(0, 0, 0);  // Mantener la rotación hacia arriba
            }
        }
    }
}