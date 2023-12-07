using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_ATTACKING = "IsAttacking";
    private Animator animator;

    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CharacterAttack characterAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, characterMovement.CheckIsWalking());
        animator.SetBool(IS_ATTACKING, characterAttack.CheckIsAttacking());
    }
    

    //animation event, can't pass bool.
    public void ExecuteAttack()
    {
        characterAttack.SetAttackArea(true);
    }

    public void EndAttack()
    {
        characterAttack.SetAttackArea(false);
    }
}
