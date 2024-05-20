using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
    public void UpdateHealthBar(float currentHealth)
    {
        float fillAmount = currentHealth / maxHealth;

        // Mettre à jour la barre de vie
        healthBarFill.fillAmount = fillAmount;
        Debug.Log("Vie actuelle: " + currentHealth);
    }
}
