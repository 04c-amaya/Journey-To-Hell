using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    EnemyManager enemyManager;

    [Header("Combat Information")]
    public GameObject projectileSpawner;
    [SerializeField] Projectile projectilePrefab;
    Projectile projectile;
    [SerializeField] int spaceBetweenTarget;

    [SerializeField] AudioClip fireClip;

    [SerializeField] float atkCooldown;
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }
    private void Start()
    {
        projectile = Instantiate(projectilePrefab);
        projectile.host = gameObject;
    }
    private void Update()
    {
        if (enemyManager.target != null)
        {
            if (enemyManager.seesPlayer)
            {
                AttackState(enemyManager.target);
            }
        }
        else
        {
            enemyManager.seesPlayer = false;
        }

    }
    void AttackState(GameObject target)
    {
        if (target.transform.position.x > transform.position.x)
        {
            //Debug.Log("going Left");
            if (transform.position.x > enemyManager.pointA.transform.position.x)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(target.transform.position.x - spaceBetweenTarget, target.transform.position.y), enemyManager.enemyMovement.speed / 1000);
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(enemyManager.pointA.transform.position.x, target.transform.position.y), enemyManager.enemyMovement.speed / 1000);
            }

            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            //Debug.Log("going Right");
            if (transform.position.x < enemyManager.pointB.transform.position.x)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(target.transform.position.x + spaceBetweenTarget, target.transform.position.y), enemyManager.enemyMovement.speed / 1000);
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(enemyManager.pointB.transform.position.x, target.transform.position.y), enemyManager.enemyMovement.speed / 1000);
            }
            transform.localScale = new Vector2(-1, 1);
        }
        if(enemyManager.isOnCooldown == false)
        {
           Invoke("EnemyProjectileLaunch", Random.Range(1, 4));
        }


    }
    public void EnemyAttackSound()
    {
        enemyManager.audioManager.PlaySFX(fireClip);
    }
    public void EnemyProjectileLaunch()
    {
        if (enemyManager.isOnCooldown == false)
        {
            enemyManager.anim.Play("Enemy Attack");
            projectile.FireProjectile();
            EnemyAttackSound();
            enemyManager.isOnCooldown = true;
            StartCoroutine(ResetAtkCooldown());
        }

    }
    IEnumerator ResetAtkCooldown()
    {
        
        yield return new WaitForSeconds(atkCooldown);
        enemyManager.isOnCooldown = false;
    }
}
