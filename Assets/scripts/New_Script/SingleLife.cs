using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLife : MonoBehaviour
{
    [SerializeField] int healthRepresetantion = 2;
    [SerializeField] List<Transform> healthPiece = new List<Transform>();

    public int GetHealthRepresentation()
    {
        return healthRepresetantion;
    }


    public void UpdateCurrentPiece(int current)
    {
        for (int i = 0; i < healthRepresetantion; i++)
        {
            healthPiece[i].gameObject.SetActive(current > i);
        }

    }
}