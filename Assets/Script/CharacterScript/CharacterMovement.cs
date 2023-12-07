using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minimunDistance;
    [SerializeField] private Transform target;
    //[SerializeField] private Transform mainTarget;
    [SerializeField] private float raycastOffest;

    [SerializeField] private string ENEMY = ""; //�Q�@�ӧ�n����k�x�s
    
    private RaycastHit2D Hit = new(); //�s�x�b�e�誺���� ���׬O�_���ĤH
    private bool IsWalking;

    private void Update()
    {
        //�g�u�P�_�O�_���ĤH
        Vector3 vec = transform.position;
        vec.x += raycastOffest;//�קK�g��ۤv

        if (gameObject.tag == "Friend")
        {
            Hit = Physics2D.Raycast(vec, transform.right, 1000);
        }
        else if(gameObject.tag == "Enemy")
        {
            Hit = Physics2D.Raycast(vec, -transform.right, 1000);
        }

        if (Hit && (Hit.collider.CompareTag("Friend") || Hit.collider.CompareTag("Enemy")))
        {
            target = Hit.collider.gameObject.transform;
        }

        if (target)
        {
            //Debug.Log(Hit.transform.position);
            if(Hit) Debug.DrawLine(vec, Hit.transform.position);

            if (Vector2.Distance(transform.position, target.position) > minimunDistance)
            {
                //�T�O��������
                Vector2 targetX = target.position;
                targetX.y = transform.position.y;

                transform.position = Vector2.MoveTowards(transform.position, targetX, speed * Time.deltaTime);
                IsWalking = true;
            }
            else
            {
                IsWalking = false;
            }
        }
        
    }

    public float GetMinimunDistance()
    {
        return minimunDistance;
    }

    public string GetEnemyTag()
    {
        return ENEMY;
    }

    public bool CheckTarget()
    {
        if (Hit) //�p�G������e��O�ĤH
        {
            GameObject target = Hit.collider.gameObject;
            return target && (target.CompareTag(ENEMY));
        }
        return false;
    }

    public bool CheckIsWalking()
    {
        return IsWalking;
    }
}
