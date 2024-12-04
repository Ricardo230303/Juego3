using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Heart : MonoBehaviour
{
    public static int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Update is called once per frame

    public static void ResetHealth()
    {
        health = 3;  // Establece la salud inicial al reiniciar
    }
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++) 
        {
            hearts[i].sprite = fullHeart;
        }
    }
}
