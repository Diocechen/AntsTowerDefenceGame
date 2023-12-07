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

    [SerializeField] private Transform hitEnemy; //�Q�g�u������̪�ĤH��Transform //�ǵ�projectile

    
    RaycastHit2D[] Hits = new RaycastHit2D[3];
    
    public override void Update()
    {
        if (characterMovement.CheckIsWalking())
        {
            SetIsAttacking(false);
            hitEnemy = null; //�M��
        }
        else
        {
            //Debug.Log(characterMovement.GetEnemyTag());
            Vector3 vec = transform.position;
            if (characterMovement.GetEnemyTag() == "Enemy")
            {
                //Debug.Log(1);
                vec.x += 4;
                Hits = Physics2D.RaycastAll(vec, transform.right, 1000); //�����Ҧ��g�u�g�쪺�F��
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
                if (hit.collider.gameObject.CompareTag(enemy)) //�ˬd�O�_���ĤH
                {
                    //Debug.Log("Shoot");
                    if (Vector2.Distance(vec, hit.collider.transform.position) < Range)
                    {
                        Debug.DrawLine(vec, hit.transform.position);
                        hitEnemy = hit.collider.gameObject.transform;

                        SetIsAttacking(true);
                        break; //�ܭ��n �_�C �`��debug�@�Ӥp��
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


    public override void SetAttackArea(bool TF) //Call from CharacterAnimator //�ݭק�
    {
        //Debug.Log(0);
        Transform projectileTran = Instantiate(projectile, shootPoint.position, Quaternion.identity);

        Projectile projectileRef = projectileTran.GetComponent<Projectile>();
        
        if (projectileRef && hitEnemy) //����I�s�Ӧh���y�����~
        {
            AudioManager.instance.PlayAudio(1, "seShoot", false);
            projectileRef.Setup(shootPoint, hitEnemy, characterMovement.GetEnemyTag(), Range);
        }
    }
}

