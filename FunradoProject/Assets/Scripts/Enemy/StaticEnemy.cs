using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MainEnemy
{
    [SerializeField]
    private Vector3[] rotatePositions;
    [SerializeField]
    private int nextRotation;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private bool isRotatable;
    private Tween idleTween;
    public bool isDead;
    public bool tweenActive;
    private void Start()
    {
        isWalkable = false;
        isRotatable = true;
        enemyCurrentState = EnemyState.Idle;
        gameManager = GameManager.instance;
        enemyText.text = "Lv. " + enemyLevel.ToString(); // Lv. XX
        UpdateLevelVisual();
    }
    public override void Attack()
    {
        idleTween.Kill();
        animatorData.animator.Play(animatorData.attackAnimation);
        transform.LookAt(new Vector3(playerComponentObject.transform.position.x, transform.position.y, playerComponentObject.transform.position.z));
    }

    public override void Patrol()
    {
        Debug.LogWarning("This is Static Enemy Can't Patrol");
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        if (!isDead)
        {

            var obj = gameManager.objectsPool.GetPooledObject(0);
            obj.transform.position = transform.position + new Vector3(0, 3, 0);
            obj.transform.GetComponent<ParticleSystem>().Play();
            isDead = true;



            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public override void Idle()
    {
        print("I am idling");
        // Enemy Idle Animation
        animatorData.animator.Play(animatorData.idleAnimation);
        if (!tweenActive && isRotatable)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            //idleTween = transform.DORotate(new Vector3(0,90, 0), rotateInterval).SetLoops(-1, LoopType.Incremental);
            idleTween = transform.DORotate(rotatePositions[nextRotation], rotateInterval).OnComplete(OnRotateFinish);
            tweenActive = true;
        }

    }

    private void OnRotateFinish()
    {
        nextRotation++;
        StartCoroutine(WaitForRotating());
        tweenActive = false;
        if (nextRotation == rotatePositions.Length)
            nextRotation = 0;
    }

    IEnumerator WaitForRotating()
    {
        isRotatable = false;
        yield return new WaitForSeconds(waitTime);
        isRotatable = true;
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
}
