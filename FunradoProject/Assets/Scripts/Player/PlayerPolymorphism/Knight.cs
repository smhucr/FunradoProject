using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MainPlayer
{
    private GameManager gameManager;

    public override void Die()
    {
        print("I'm Dead Bruah");
        playerCurrentState = PlayerState.Die;
        GameManager.instance.GameOver();
    }

    private void Awake()
    {
        gameManager = GameManager.instance;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedKey"))
        {
            gameManager.redKey.SetActive(true);
            gameManager.countOfRedKeys++;
            other.gameObject.SetActive(false);
            gameManager.isRedClaimed = true;
            gameManager.UpdateKeysCount();
        }
        else if (other.CompareTag("BlueKey"))
        {
            gameManager.blueKey.SetActive(true);
            gameManager.countOfBlueKeys++;
            other.gameObject.SetActive(false);
            gameManager.isBlueClaimed = true;
            gameManager.UpdateKeysCount();
        }
        else if (other.CompareTag("Enemy"))
        {
            print("EnemyCollided");
            if (other.GetComponent<MainEnemy>().enemyCurrentState != MainEnemy.EnemyState.Attack)
            {
                playerCurrentState = PlayerState.Attack;
                StartCoroutine(WaitForAttackAnimation(other.gameObject));
                other.GetComponent<MainEnemy>().enemyCurrentState = MainEnemy.EnemyState.Attack;
            }
        }
        else if (other.CompareTag("RedArea"))
        {
            if (other.transform.parent.GetComponent<MainEnemy>().enemyCurrentState != MainEnemy.EnemyState.Attack)
            {
                playerCurrentState = PlayerState.Attack;
                StartCoroutine(WaitForAttackAnimation(other.transform.parent.gameObject));
                other.transform.parent.GetComponent<MainEnemy>().enemyCurrentState = MainEnemy.EnemyState.Attack;

            }
        }
    }

    IEnumerator WaitForAttackAnimation(GameObject enemy)
    {
        yield return new WaitForSeconds(1.3f);
        if (enemy.GetComponent<MainEnemy>().enemyLevel > Level)
        {
            playerCurrentState = PlayerState.Die;
        }
        else
        {
            enemy.GetComponent<MainEnemy>().enemyCurrentState = MainEnemy.EnemyState.Die;
            IncreaseLevel(enemy.GetComponent<MainEnemy>().enemyLevel);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Doors"))
        {
            collision.gameObject.GetComponent<DoorChecker>().OpenDoors();
        }
        print("Collision Detect");
    }
}
