using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainEnemy : MonoBehaviour
{
    private GameManager gameManager;
    [Header("MainPlayer")]
    public Transform playerComponentObject;
    public Transform playerFollowObject;
    [Header("EnemyFeatures")]
    [SerializeField]
    public float moveSpeed;

    [Header("Animation")]
    public AnimatorData animatorData;


    //State Machine
    public enum EnemyState
    {
        Idle,
        Patrol,
        Attack,
        Die
    }
    public EnemyState enemyCurrentState = EnemyState.Idle;

    private void Awake()
    {
        playerComponentObject = GameManager.instance.mainPlayer.transform;
        gameManager = GameManager.instance;
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameOver && enemyCurrentState != EnemyState.Die)
        {
            enemyCurrentState = EnemyState.Idle;
            Idle();
        }
        else
        {
            switch (enemyCurrentState)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Patrol:
                    Chase();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Die:
                    Die();
                    break;
            }
        }


    }

    public abstract void Idle();
    public abstract void Chase();
    public abstract void Attack();
    public abstract void Die();

    public void PlayIdleAnimation()
    {
        enemyCurrentState = EnemyState.Idle;
    }
    public void PlayPatrolAnimation()
    {
        enemyCurrentState = EnemyState.Patrol;
    }
    

}
