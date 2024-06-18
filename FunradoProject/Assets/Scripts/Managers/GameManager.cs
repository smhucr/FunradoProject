using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Transactions;
using DG.Tweening.Core.Easing;
public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public static GameManager instance;
    public ObjectsPool objectsPool;
    [Header("UI")]
    public GameObject redKey;
    public int countOfRedKeys;
    public TextMeshProUGUI countRedKeysText;
    public bool isRedClaimed = false;
    public GameObject blueKey;
    public int countOfBlueKeys;
    public TextMeshProUGUI countBlueKeysText;
    public bool isBlueClaimed = false;
    [Header("Player")]
    public GameObject playerParent; // Moving Player
    public GameObject mainPlayer; // Player who has script features

    [Header("Player Settings")]
    public float playerSpeed = 5f;
    public DynamicJoystick joystick;
    public Rigidbody rb;
    [Header("Enemy")]

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    [Header("ControlCheckers")]
    public bool startGame;
    public bool isGameOver;
    public bool isMoveable;
    private void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        MakeInstance();
        StartGame();

        //Assing Level
        if (PlayerPrefs.GetInt("Level") == 0)
            PlayerPrefs.SetInt("Level", 1);
        /*if (levelText != null)
            levelText.text = ("Level " + PlayerPrefs.GetInt("Level")).ToString();*/

        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UpdateKeysCount();
    }

    public void StartGame()
    {
        startGame = true;
        isMoveable = true;

    }

    public void WinGame()
    {
        isGameOver = true;
        startGame = false;
        isMoveable = false;

    }

    public void GameOver()
    {
        isGameOver = true;
        startGame = false;

        isMoveable = false;
    }

    public void UpdateKeysCount()
    {
        countBlueKeysText.text = countOfBlueKeys.ToString();
        countRedKeysText.text = countOfRedKeys.ToString();
    }

    public IEnumerator DisableMoveable(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMoveable = false;
    }

}
