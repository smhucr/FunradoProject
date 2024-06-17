using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MainEnemy
{
    [SerializeField]
    private int nextPoint = 1;
    [SerializeField]
    private float waitTime;

    private Tween idleTween;
    
    private void Start()
    {
        isWalkable = true;
        waitTime = 4;
        nextPoint = 1;
        enemyCurrentState = EnemyState.Patrol;
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
            gameObject.transform.DOMove(patrolPoints[nextPoint].transform.position, moveTime).SetEase(Ease.Linear).OnComplete(WalkFinish);
            gameObject.transform.LookAt(new Vector3(patrolPoints[nextPoint].transform.position.x, 0, patrolPoints[nextPoint].transform.position.z));
            isWalking = true;
        }
    }

    public void WalkFinish()
    {
        isWalking = false;
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
        throw new System.NotImplementedException();
    }


    public override void Die()
    {
        print("I am dying");
        //Enemy Die Animation After Die Animation Destroy the Object

        animatorData.animator.Play(animatorData.deathAnimation);
    }


}
