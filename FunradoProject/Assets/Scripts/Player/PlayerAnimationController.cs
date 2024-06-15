using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    GameManager gameManager;
    public AnimatorData animatorData;
    private MainPlayer mainPlayer;
    private void Start()
    {
        gameManager = GameManager.instance;
        mainPlayer = gameObject.GetComponent<MainPlayer>();
    }
    private void FixedUpdate()
    {
        if (gameManager.isGameOver && mainPlayer.playerCurrentState != MainPlayer.PlayerState.Die)
        {
            mainPlayer.playerCurrentState = MainPlayer.PlayerState.Idle;
            Idle();
        }
        else
        {
            switch (mainPlayer.playerCurrentState)
            {
                case MainPlayer.PlayerState.Idle:
                    Idle();
                    break;
                case MainPlayer.PlayerState.Walk:
                    Walk();
                    break;
                case MainPlayer.PlayerState.Die:
                    Die();
                    break;
            }
        }

    }

    private void Idle()
    {
        animatorData.animator.Play(animatorData.idleAnimation);
    }
    private void Walk()
    {
        animatorData.animator.Play(animatorData.walkAnimation);
    }
    private void Die()
    {
        animatorData.animator.Play(animatorData.deathAnimation);
    }
}
