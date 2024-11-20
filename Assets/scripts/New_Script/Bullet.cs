using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Daño de la bala
    public float bulletLife = 2f;  // Tiempo de vida de la bala en segundos

    private void Start()
    {
        // Destruir la bala después de 'bulletLife' segundos
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto con el que ha colisionado es un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Obtener el componente del enemigo (suponiendo que tenga un script Enemy con un método TakeDamage)
            Boss_Phase enemy = other.GetComponent<Boss_Phase>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Aplicar el daño al enemigo
            }
        }

        // Destruir la bala cuando colisione con cualquier objeto
        Destroy(gameObject);
    }
}