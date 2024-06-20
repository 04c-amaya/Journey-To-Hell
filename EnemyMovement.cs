using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager enemyManager;
    [Header("Patrol Path")]
    bool isAtPointA;
    public float speed;

    [SerializeField] float maxDistFromPath;
    [SerializeField] float maxDistFromPlayer;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }
    private void Update()
    {
        if (enemyManager.enemyStats.isDead)
            return;

        if (!enemyManager.seesPlayer)
        {
            Patrol();
        }
        DistFromTarget();
    }
    void DistFromTarget()
    {
        var middleOfPath = (enemyManager.pointB.transform.position + enemyManager.pointA.transform.position) / 2;
        if (enemyManager.target != null)
        {
           var test_distanceFromPlayer = Vector3.Distance(transform.position, enemyManager.target.transform.position);
            if (test_distanceFromPlayer > maxDistFromPlayer)
            {
                enemyManager.seesPlayer = false;
                enemyManager.target = null;
                Patrol();

            }
        }
       var test_distanceFromPath = Vector3.Distance(transform.position, middleOfPath);
        if (test_distanceFromPath > maxDistFromPath)
        {
            enemyManager.seesPlayer = false;
            enemyManager.target = null;
            Patrol();

        }
    }
    void Patrol()
    {

        if (isAtPointA)
        {
            transform.position = Vector2.Lerp(transform.position, enemyManager.pointB.transform.position, speed / 1000);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, enemyManager.pointA.transform.position, speed / 1000);
            transform.localScale = new Vector2(-1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "PointA")
        {
            isAtPointA = true;
        }
        if (collision.gameObject.name == "PointB")
        {
            isAtPointA = false;
        }
    }
}
