using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; 

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
