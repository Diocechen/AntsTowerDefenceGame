using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : CharacterHealth
{
    public override void GetDamaged(float DamageAmount)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= DamageAmount;
        }

        UpdateHealthBar(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                WinLoseHandleSystem.instance.GameOver(true);
            }
            else if (gameObject.CompareTag("Friend"))
            {
                WinLoseHandleSystem.instance.GameOver(false);
            }
        }
    }
}
