using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeleeAttack : CharacterAttack
{
    //[SerializeField] private CharacterMovement characterMovement;
    //private bool IsAttacking;

    [SerializeField] private float Damage;
    [SerializeField] private AttackArea attackArea;
    private void Start()
    {
        attackArea.SetDamage(Damage);
    }

    public override void Update()
    {

        if (characterMovement.CheckIsWalking())
        {
            SetIsAttacking(false);
            SetAttackArea(false); //Á×§Kbug 
        }
        else
        {
            if (characterMovement.CheckTarget())
            {
                SetIsAttacking(true);
            }
        }
    }

    public override void SetAttackArea(bool TF)
    {
        if (TF)
        {
            attackArea.EnableArea();
        }
        else
        {
            attackArea.DisableArea();
        }
    }
}
