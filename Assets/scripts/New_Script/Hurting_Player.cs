using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurting_Player : MonoBehaviour
{
    [SerializeField] GameOver gameOverScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy_Bullet"))
        {
            Health_Heart.health--;
            if(Health_Heart.health <= 0 )
            {
                gameOverScript.ShowGameOverMenu();
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
    }
    IEnumerator GetHurt()
    {
        Physics.IgnoreLayerCollision(7, 8);
        yield return new WaitForSeconds(2);
        Physics.IgnoreLayerCollision(7, 8, false);
    }
}
