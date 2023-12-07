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

    public virtual void SetAttackArea(bool TF) //讓CharacterAnimator存取，再想想有沒有更好的辦法 
    {
        if (TF)
        {
            //依據需求改寫
        }
        else
        {
            
        }
    }
}
