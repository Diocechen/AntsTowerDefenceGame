using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float CurrentHealth;

    [SerializeField] private Slider healthBar;
    [SerializeField] private int foodInAnt;

    [SerializeField] private DeathHandleSystem deathHandleSystem;

    private void Awake()
    {
        //我覺得這不是一個好方法，但是我想不到更好的了。
        GameObject deathHandleSystemObj = GameObject.FindGameObjectsWithTag("DeathHandleSystem")[0];
        deathHandleSystem = deathHandleSystemObj.GetComponent<DeathHandleSystem>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthBar(CurrentHealth, MaxHealth);
    }

    public void UpdateHealthBar(float CurrentValue, float MaxValue)
    {
        healthBar.value = CurrentValue / MaxValue;
    }

    public virtual void GetDamaged(float DamageAmount)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= DamageAmount;
        }

        UpdateHealthBar(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        if (deathHandleSystem)
        {
            deathHandleSystem.AnAntDied(foodInAnt, gameObject.tag);
            Destroy(gameObject);
        }
    }

}
