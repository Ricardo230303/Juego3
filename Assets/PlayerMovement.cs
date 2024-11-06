using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float groundDist;
    public float jumpForce;  // Fuerza de salto
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    private Animator animator;

    private bool isGrounded;  // Para verificar si el jugador está en el suelo
    private float groundCheckRadius = 0.3f; // Radio para comprobar si está tocando el suelo
    [SerializeField] float changInYAxisAnimation = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Manejo del movimiento horizontal
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y).normalized;

        // Aplica la velocidad en el plano horizontal
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed); // Mantiene la componente Y (caída) actual

        // Animación de caminar
        animator.SetBool("isWalking", moveDir.magnitude > 0);
        animator.SetFloat("falling", rb.velocity.y > changInYAxisAnimation ? +1 : rb.velocity.y < -changInYAxisAnimation ?  - 1 : 0);

        // Controlar la dirección del sprite
        if (x != 0)
        {
            sr.flipX = x < 0;
        }

        // Mecanismo de salto
        if (isGrounded && Input.GetButtonDown("Jump")) // Verifica si está en el suelo y se presiona el salto
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplica la fuerza de salto
        }
    }

    void FixedUpdate()
    {
        // Verifica si el jugador está tocando el suelo
        isGrounded = IsGrounded();

        // Raycast para mantener la altura del jugador en relación al terreno
        RaycastHit hit;
        Vector3 castPos = transform.position + Vector3.up; // Ajusta la posición del raycast
        if (Physics.Raycast(castPos, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            // Ajusta la altura solo si está por debajo del terreno
            if (transform.position.y < hit.point.y + groundDist)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                rb.MovePosition(movePos); // Mueve el Rigidbody en lugar de la transform
            }
        }
    }

    // Verifica si el jugador está tocando el suelo utilizando un pequeño raycast hacia abajo
    bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position - Vector3.up * 0.5f, groundCheckRadius, terrainLayer);
    }
    // Dibuja el Gizmo para mostrar la comprobación del suelo
    private void OnDrawGizmos()
    {
        if (terrainLayer != 0)
        {
            Gizmos.color = Color.green; // Color del Gizmo
            Gizmos.DrawWireSphere(transform.position - Vector3.up * 0.5f, groundCheckRadius); // Dibuja la esfera de comprobación
        }
    }
}
