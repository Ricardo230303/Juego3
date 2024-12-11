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

            Mayo_Health mayo = other.GetComponent<Mayo_Health>();

            if (mayo != null)
            {
                mayo.TakeDamage(damage);
            }

            Palta palta = other.GetComponent<Palta>();
            Debug.Log(other.name);
            if (palta == null) {
            
                if(other.transform.parent != null) {
                    palta = other.transform.parent.GetComponent<Palta>();
                    Debug.Log(other.name);
                }
            }

            if (palta != null)
            {
                palta.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Destruir la bala cuando colisiona con el layer Ground
            Destroy(gameObject);
        }
    }
}