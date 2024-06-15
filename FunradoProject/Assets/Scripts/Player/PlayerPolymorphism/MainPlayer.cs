using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [Header("CharacterFeatures")]
    [SerializeField]
    public int level;


    public enum PlayerState
    {
        Idle,
        Walk,
        Die
    }
    public PlayerState playerCurrentState = PlayerState.Idle;

    public void Die()
    {
        print("I'm Dead Bruah");
        playerCurrentState = PlayerState.Die;
        GameManager.instance.GameOver();
    }

    public void Reborn()
    {
        //This is a ADS part
    }
}
