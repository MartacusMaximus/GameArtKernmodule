using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public Image barImage;
    bool isDead;
    private float barFill;
    

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        barFill = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        barImage.fillAmount = barFill;
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }
        else
        {
            currentHealth -= amount;
            barFill = currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        print("I am Dead");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
