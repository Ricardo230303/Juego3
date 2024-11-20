using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int currentHealth;
    [SerializeField] UnityEvent onEnemyDeath;
    [SerializeField] Transform hearthContainer;
    [SerializeField] SingleLife singleHearth;
    [SerializeField] GameOver gameOverScript;


    [SerializeField] List<SingleLife> currentHealthVisual;

    public void Start()
    {
        currentHealth = health;
        UpdateCurrentHeath();
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        UpdateCurrentHeath();
        if (currentHealth <= 0)
        {
            gameOverScript.ShowGameOverMenu();
        }
    }

    public void Heal(int damage)
    {
        UpdateCurrentHeath();
        currentHealth += damage;
    }

    void UpdateCurrentHeath()
    {
        if (currentHealthVisual.Count == 0)
        {
            for (int i = 0; i < health / 2; i++)
            {
                SingleLife sh = Instantiate(singleHearth);
                sh.transform.SetParent(hearthContainer);
                sh.gameObject.SetActive(true);
                sh.transform.localScale = Vector3.one;
                currentHealthVisual.Add(sh);
            }
        }
        int current = currentHealth;
        for (int i = 0; i < health / 2; i++)
        {
            if (current <= 1)
            {
                if (current <= 0)
                {
                    current = 0;
                }
                currentHealthVisual[i].UpdateCurrentPiece(current);

            }
            current -= 2;
        }
    }
}