﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false;

    public SpriteRenderer graphics;

    public HealthBar healthBar;


    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance!= null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Player Health dans la scène.");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }else
        {
            currentHealth += amount;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}