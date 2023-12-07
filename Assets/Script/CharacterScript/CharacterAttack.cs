using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public CharacterMovement characterMovement;
    private bool IsAttacking;

    public virtual void Update()
    {
        if (characterMovement.CheckIsWalking())
        {
            SetIsAttacking(false);
        }
        else
        {
            if (characterMovement.CheckTarget())
            {
                SetIsAttacking(true);
            }
        }
    }

    public void SetIsAttacking(bool TF)
    {
        IsAttacking = TF;
    }

    public bool CheckIsAttacking()
    {
        return IsAttacking;
    }

    public virtual void SetAttackArea(bool TF) //��CharacterAnimator�s���A�A�Q�Q���S����n����k 
    {
        if (TF)
        {
            //�̾ڻݨD��g
        }
        else
        {
            
        }
    }
}
