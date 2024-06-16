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
