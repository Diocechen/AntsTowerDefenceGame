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

    [SerializeField] private string ENEMY = ""; //想一個更好的方法儲存
    
    private RaycastHit2D Hit = new(); //存儲在前方的螞蟻 不論是否為敵人
    private bool IsWalking;

    private void Update()
    {
        //射線判斷是否有敵人
        Vector3 vec = transform.position;
        vec.x += raycastOffest;//避免射到自己

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
                //確保水平移動
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
        if (Hit) //如果偵測到前方是敵人
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
