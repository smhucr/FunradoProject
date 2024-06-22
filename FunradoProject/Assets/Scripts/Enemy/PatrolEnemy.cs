using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MainEnemy
{
    [Header("Patrol")]
    public GameObject[] patrolPoints;
    [SerializeField]
    private int nextPoint;
    [SerializeField]
    private float waitTime;

    private Tween idleTween;
    private Tween walkTween;
    public bool isDead;

    private void Start()
    {
        isWalkable = true;
        waitTime = 4;
        enemyCurrentState = EnemyState.Patrol;
        gameManager = GameManager.instance;
        enemyText.text = "Lv. " + enemyLevel.ToString(); // Lv. XX
        UpdateLevelVisual();
    }

    public override void UpdateLevelVisual()
    {
        LevelPresenter playerLevelPresenter = playerComponentObject.GetComponent<LevelPresenter>();
        if (playerLevelPresenter.Level > enemyLevel)
        {
            enemyText.color = Color.green; 
        }
        else
        {
            enemyText.color = Color.red;
        }
    }

    public override void Idle()
    {
        print("I am idling");
        // Enemy Idle Animation
        animatorData.animator.Play(animatorData.idleAnimation);
        if (idleTween == null)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            //idleTween = transform.DORotate(new Vector3(0,90, 0), rotateInterval).SetLoops(-1, LoopType.Incremental);
            idleTween = transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y + 90, 0), rotateInterval).SetLoops(-1, LoopType.Incremental);
        }
    }
    public override void Patrol()
    {
        if (!isWalking && isWalkable)
        {
            idleTween.Kill();
            idleTween = null;
            print("I am patrolling");
            animatorData.animator.Play(animatorData.walkAnimation);
            walkTween = gameObject.transform.DOMove(patrolPoints[nextPoint].transform.position, moveTime).SetEase(Ease.Linear).OnComplete(WalkFinish);
            gameObject.transform.LookAt(new Vector3(patrolPoints[nextPoint].transform.position.x, 0, patrolPoints[nextPoint].transform.position.z));
            isWalking = true;
        }
    }

    public void WalkFinish()
    {
        isWalking = false;
        if (gameObject.activeSelf)
            StartCoroutine(WaitForPatrolling());
        nextPoint++;
        if (nextPoint == patrolPoints.Length)
            nextPoint = 0;
    }

    IEnumerator WaitForPatrolling()
    {
        isWalkable = false;
        Idle();
        yield return new WaitForSeconds(waitTime);
        isWalkable = true;
        Patrol();
    }

    public override void Attack()
    {
        idleTween.Kill();
        walkTween.Kill();
        animatorData.animator.Play(animatorData.attackAnimation);
        transform.LookAt(new Vector3(playerComponentObject.transform.position.x, transform.position.y, playerComponentObject.transform.position.z));

    }

    public override void Die()
    {
        print("I am dying");
        //Enemy Die Animation After Die Animation Destroy the Object
        //animatorData.animator.Play(animatorData.deathAnimation); //NONE DeathAnimation, Therefore animation will be Hand-made
        if (!isDead)
        {

            var obj = gameManager.objectsPool.GetPooledObject(0);
            obj.transform.position = transform.position + new Vector3(0, 3, 0);
            obj.transform.GetComponent<ParticleSystem>().Play();
            isDead = true;



            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }


}
