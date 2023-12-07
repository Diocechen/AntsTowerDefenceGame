using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ParticleSystem bloodEffect;

    [SerializeField] private Transform shootPoint; //定點
    [SerializeField] private Transform shootThisEnemy;
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;
    private string ENEMY = "";
    private float range = 10; //跟前一個角色的距離

    //private float shootPointX;
    //private float shootThisEnemyX;
    //private float shootThisEnemyY;
    //private float nextX;

    private bool SetUpDone = false;

    private Rigidbody2D rb;

    private void Start()
    {
        if (shootThisEnemy)
        {
            rb = GetComponent<Rigidbody2D>();
            // 计算抛射方向和初始速度

            Vector3 newPos = shootThisEnemy.position;
            newPos.y += 5;

            Vector3 direction = (newPos - shootPoint.position).normalized;

            float speed;
            float dis = Vector2.Distance(shootPoint.position, shootThisEnemy.position);
            if (dis > range) //透過距離判斷需要多少速度
            {
                //Debug.Log(dis);
                //Debug.Log("1");
                speed = Speed * 2;
            }
            else
            {
                //Debug.Log("2");
                speed = Speed;
            }

            rb.velocity = direction * speed * Time.fixedDeltaTime;

            SetUpDone = true;
        }
        else //沒有目標物時生成投射物
        {
            rb = GetComponent<Rigidbody2D>();
            // 计算抛射方向和初始速度

            Vector3 newPos = transform.position;
            newPos.y += 5;

            
            if (gameObject.CompareTag("Enemy")) //透過tag處理前進，想個更好的方法。
            {
                newPos.x -= 100; //左
            }
            else
            {
                newPos.x += 100; //右
            }
            

            Vector3 direction = (newPos - transform.position).normalized;

            float speed = Speed;

            rb.velocity = direction * speed * Time.fixedDeltaTime;

            SetUpDone = true;
        }
        
    }

    private void Update()
    {
        //會出問題 先註解起來
        /*if (SetUpDone)
        {
            float distance = Math.Abs(shootThisEnemyX - shootPointX);

            if (ENEMY == "Enemy") //調整距離用
            {
                nextX = Mathf.MoveTowards(transform.position.x, shootThisEnemyX + 1, Speed * Time.deltaTime);
                //MathF.MoveTowards用於浮點數之間的平滑線性插值
            }
            else
            {
                nextX = Mathf.MoveTowards(transform.position.x, shootThisEnemyX, Speed * Time.deltaTime);
            }

            /

            float baseY = Mathf.Lerp(transform.position.y, shootThisEnemyY-11, ((nextX - transform.position.x) / distance));
            //lerp 線性插植
            //用於在兩個值之間進行平滑的線性過渡。
            //Debug.Log(transform.position.y);
            Debug.Log(shootThisEnemyY);

            float height = 5f * (nextX - transform.position.x) * (nextX - shootThisEnemyX) / (-0.25f * distance * distance);
            //不知道哪裡來的式子
            //決定投射物的頂點

            Vector3 movePosition = new(nextX, baseY+height, transform.position.z); //下一幀要移動到的點

            //將所有計算出的結果輸入到投射物上
            transform.rotation = LookAtTarget(movePosition - transform.position);
            transform.position = movePosition;/

            float distanceCovered = (Time.time - startTime) * Speed;
            float fractionOfJourney = distanceCovered / distance;
        }*/

    }

    private void FixedUpdate()
    {
        if (SetUpDone)
        {
            // 應用重力
            rb.AddForce(Vector3.down * Physics.gravity.magnitude, ForceMode2D.Force);
        }
    }

    public Quaternion LookAtTarget(Vector2 rotation) //投射時的旋轉
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
        //Mathf.Atan2是Unity中的一個數學函數，用於計算給定點的反正切值
        //正切 對邊 / 鄰邊
        //反正切 = tan^-1
        //函數返回的結果是一個弧度值

        //Mathf.Rad2Deg 將弧度轉成角度
    }

    public void Setup(Transform shootPoint, Transform shootThisEnemy, string enemyTag, float range)
    {
        this.shootPoint = shootPoint;

        this.shootThisEnemy = shootThisEnemy;

        //this.range = range;

        //Debug.Log(enemyTag);
        //shootPointX = shootPoint.position.x;
        //shootThisEnemyX = shootThisEnemy.position.x;
        //shootThisEnemyY = shootThisEnemy.position.y;
        //Debug.Log(shootThisEnemyY);
        ENEMY = enemyTag;
        //Debug.Log(range);
        //SetUpDone = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ENEMY != null && ENEMY != "")
        {
            if (collision.CompareTag(ENEMY))
            {
                //apply damage to collision
                if (collision.TryGetComponent<CharacterHealth>(out var enemyHealth))
                {
                    AudioManager.instance.PlayAudio(3, "seProjectileHit", false);
                    Instantiate(bloodEffect, transform.position, Quaternion.identity);
                    enemyHealth.GetDamaged(Damage);
                }
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }

}
