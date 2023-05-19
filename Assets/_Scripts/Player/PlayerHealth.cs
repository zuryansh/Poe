using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour , I_TakeDamage
{
    [SerializeField] int currentHealth;
    [SerializeField] Player player;
    public static event Action<int> onHealthChange;


    public int MaxHealth => player.Data.maxHealth;
    public int CurrentHealth => currentHealth;


    // Start is called before the first frame update
    void Start()
    {

        UpdatePlayerHealth(player.Data.maxHealth , gameObject);
    }


    public void TakeDamage(int val , GameObject source)
    {
        UpdatePlayerHealth(-val , source);
    }

    public void UpdatePlayerHealth(int health , GameObject source)
    {

        if (health < 0)
        {
            if(player.IsInvincible) return;
            OnPlayerHit(source);
        }

        currentHealth += health;

        if(currentHealth > player.Data.maxHealth) health = player.Data.maxHealth;
        if(currentHealth <= 0) Die();

        onHealthChange?.Invoke(currentHealth);
    }


    void OnPlayerHit(GameObject source)
    {
        player.HitSource = source;
        player.IsInvincible = true;
        player.SetState(player.hitState);

        Invoke("ResetInvincibility", player.InvincibilityTime);
    }

    void ResetInvincibility() => player.IsInvincible = false;

    void Die()
    {
        Debug.Log("DIED");
    }
}
