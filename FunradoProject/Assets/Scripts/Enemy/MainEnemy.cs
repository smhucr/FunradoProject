using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MainEnemy : MonoBehaviour
{
    protected GameManager gameManager;
    [Header("MainPlayer")]
    public Transform playerComponentObject;
    [Header("EnemyFeatures")]
    public int enemyLevel;
    public TextMeshProUGUI enemyText;
    [SerializeField]
    public float moveTime;
    public float rotateInterval;
    public bool isWalking;
    public bool isWalkable = true;
    [Header("Animation")]
    public AnimatorData animatorData;
    [Header("Patrol")]
    public GameObject[] patrolPoints;



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
        playerComponentObject.GetComponent<LevelPresenter>().OnLevelIncrease += UpdateLevelVisual;
    }

    private void OnDisable()
    {
        playerComponentObject.GetComponent<LevelPresenter>().OnLevelIncrease -= UpdateLevelVisual;
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameOver)
            DOTween.KillAll();
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
                    Patrol();
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
    public abstract void Patrol();
    public abstract void Attack();
    public abstract void Die();
    public abstract void UpdateLevelVisual();

    public void PlayIdleAnimation()
    {
        enemyCurrentState = EnemyState.Idle;
    }
    public void PlayPatrolAnimation()
    {
        enemyCurrentState = EnemyState.Patrol;
    }
    

}
