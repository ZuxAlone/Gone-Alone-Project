using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Sprite emptyHeart;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Image[] hearts;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    
    public void InitializeHealth(int initHealth)
    {
        maxHealth = initHealth;
        health = initHealth;
    }

    public void UpdateHealth(int currentHealth) 
    {
        health = currentHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
