using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    public event Action OnDoorOpened;
    private GameManager gameManager;

    public bool isThisRedDoor;
    public bool isThisBlueDoor;

    private BoxCollider boxCollider;

    private void Start()
    {
        gameManager = GameManager.instance;
        boxCollider = GetComponent<BoxCollider>();
    }

    public void OpenDoors()
    {
        if (isThisRedDoor && gameManager.isRedClaimed)
        {
            gameManager.countOfRedKeys--;
            if (gameManager.countOfRedKeys == 0)
            {
                gameManager.redKey.SetActive(false);
                gameManager.isRedClaimed = false;
            }
            boxCollider.enabled = false;
            OnDoorOpened?.Invoke();

        }
        else if (isThisBlueDoor && gameManager.isBlueClaimed)
        {
            gameManager.countOfBlueKeys--;
            if (gameManager.countOfBlueKeys == 0)
            {
                gameManager.blueKey.SetActive(false);
                gameManager.isBlueClaimed = false;
            }
            boxCollider.enabled = false;
            OnDoorOpened?.Invoke();
        }

        gameManager.UpdateKeysCount();

    }
}
