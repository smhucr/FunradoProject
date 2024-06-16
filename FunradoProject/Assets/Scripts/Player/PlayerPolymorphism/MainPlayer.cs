using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainPlayer : MonoBehaviour
{
    [Header("CharacterFeatures")]
    [SerializeField]
    private int level;
    public event Action OnLevelChange;
    public int Level
    {
        get => level;
        set => level = value; 
    }

    public enum PlayerState
    {
        Idle,
        Walk,
        Die
    }
    public PlayerState playerCurrentState = PlayerState.Idle;

    public abstract void Die();
    public  void IncreaseLevel(int increaseAmount)
    {
        print("Level Increased");
        level += increaseAmount;
        OnLevelChange?.Invoke();
    }

    /*public void Reborn()
    {
        //This is a ADS part
    }*/
}
