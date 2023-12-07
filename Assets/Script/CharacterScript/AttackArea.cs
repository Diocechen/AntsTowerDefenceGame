using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] ParticleSystem bloodEffect;

    [SerializeField] Collider2D area;
    private float Damage;
    [SerializeField] private string ENEMY = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ENEMY)
        {
            //Debug.Log(ENEMY);
            //apply damage to collision
            CharacterHealth enemyHealth = collision.GetComponent<CharacterHealth>();
            if (enemyHealth != null)
            {
                AudioManager.instance.PlayAudio(2, "seAntHit", false);
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
                enemyHealth.GetDamaged(Damage);
            }
            area.enabled = false;
        }
        
    }

    public void EnableArea()
    {
        area.enabled = true;
    }

    public void DisableArea()
    {
        area.enabled = false;
    }

    public void SetDamage(float Dam)
    {
        Damage = Dam;
    }
}
