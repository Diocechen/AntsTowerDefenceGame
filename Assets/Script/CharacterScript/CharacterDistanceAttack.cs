using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterDistanceAttack : CharacterAttack
{
    [SerializeField] private float Range;
    [SerializeField] private Transform projectile;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private Transform hitEnemy; //被射線偵測到最近敵人的Transform //傳給projectile

    
    RaycastHit2D[] Hits = new RaycastHit2D[3];
    
    public override void Update()
    {
        if (characterMovement.CheckIsWalking())
        {
            SetIsAttacking(false);
            hitEnemy = null; //清空
        }
        else
        {
            //Debug.Log(characterMovement.GetEnemyTag());
            Vector3 vec = transform.position;
            if (characterMovement.GetEnemyTag() == "Enemy")
            {
                //Debug.Log(1);
                vec.x += 4;
                Hits = Physics2D.RaycastAll(vec, transform.right, 1000); //紀錄所有射線射到的東西
            }
            else
            {
                //Debug.Log(2);
                vec.x -= 4;
                Hits = Physics2D.RaycastAll(vec, -transform.right, 1000);
            }
            

            foreach (RaycastHit2D hit in Hits)
            {
                string enemy = characterMovement.GetEnemyTag();
                //Debug.Log(1+enemy);
                //Debug.Log(hit.collider.gameObject.tag);
                //Debug.DrawLine(vec, hit.transform.position);
                if (hit.collider.gameObject.CompareTag(enemy)) //檢查是否為敵人
                {
                    //Debug.Log("Shoot");
                    if (Vector2.Distance(vec, hit.collider.transform.position) < Range)
                    {
                        Debug.DrawLine(vec, hit.transform.position);
                        hitEnemy = hit.collider.gameObject.transform;

                        SetIsAttacking(true);
                        break; //很重要 北七 害我debug一個小時
                    }
                }
            }
        }
    }

    /*public void Shoot() 
    {
        Instantiate(projectile, shootPosition.position, Quaternion.identity);
    }*/

    public float GetRange()
    {
        return Range;
    }


    public override void SetAttackArea(bool TF) //Call from CharacterAnimator //待修改
    {
        //Debug.Log(0);
        Transform projectileTran = Instantiate(projectile, shootPoint.position, Quaternion.identity);

        Projectile projectileRef = projectileTran.GetComponent<Projectile>();
        
        if (projectileRef && hitEnemy) //防止呼叫太多次造成錯誤
        {
            AudioManager.instance.PlayAudio(1, "seShoot", false);
            projectileRef.Setup(shootPoint, hitEnemy, characterMovement.GetEnemyTag(), Range);
        }
    }
}

