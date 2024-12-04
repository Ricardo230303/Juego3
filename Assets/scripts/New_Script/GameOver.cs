using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0; // Pausa el juego cuando se muestra el menú de game over
    }
  
    public void RestartGame()
    {
        Health_Heart.ResetHealth();
        Time.timeScale = 1; // Reanuda el tiempo antes de cargar la escena para asegurarse de que el juego se reinicie correctamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     /*
        HealthSystem playerHealth = FindObjectOfType<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.ResetPlayerHealth();
        }
    */
    }

  /*  public void GoToMenu()
    {
        Time.timeScale = 1; // Reanuda el tiempo antes de cargar la escena para asegurarse de que el juego se reinicie correctamente
        SceneManager.LoadScene("Start");

        HealthSystem playerHealth = FindObjectOfType<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.ResetPlayerHealth();
        }
  */
 
}
