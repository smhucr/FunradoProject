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
}
