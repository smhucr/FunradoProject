using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPresenter : MonoBehaviour
{
    public event Action OnLevelIncrease;

    [Header("Model")]
    [SerializeField]
    private MainPlayer player;
    [SerializeField]
    private int level = 0; // Customable Per Level

    public int Level { get { return level; } }

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI levelText; // Lv. XX

    private void Start()
    {
        player = GetComponent<MainPlayer>();
        level = player.Level;
        UpdateLevelView();
        player.OnLevelChange += Level_OnLevelChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            IncreaseLevel(other.transform.GetComponent<LevelCollectableData>().levelAmount);
            other.gameObject.SetActive(false);
        }
    }

    private void Level_OnLevelChange()
    {
        level = player.Level;
        UpdateLevelView();
    }

    public void IncreaseLevel(int amount)
    {
        player?.IncreaseLevel(amount);
        OnLevelIncrease?.Invoke(); //Can be useful
    }

    public void UpdateLevelView()
    {
        if (player == null)
            return;
        levelText.text = "Lv. " + level.ToString();
    }

    private void OnDisable()
    {
        player.OnLevelChange -= Level_OnLevelChange;
    }
}
