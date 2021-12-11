using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyField;
    [SerializeField]
    private TMP_Text fishField;

    [SerializeField]
    private TMP_Text foodCostField;
    [SerializeField]
    private TMP_Text fishCostField;
    
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        GameManager.OnMoneyChanged += RefreshMoneyUI;
        FishManager.OnFishCountChanged += RefreshFishUI;
        FishManager.OnFishCostChanged += UpdateFishCost;
    }

    private void UpdateFishCost(int value)
    {
        fishCostField.text =  $"€ {value}";
    }

    private void OnDisable()
    {
        GameManager.OnMoneyChanged -= RefreshMoneyUI;
        FishManager.OnFishCountChanged -= RefreshFishUI;
        FishManager.OnFishCostChanged -= UpdateFishCost;
    }

    private void RefreshFishUI(int value)
    {
        fishField.text = value.ToString();
    }

    private void RefreshMoneyUI(int value)
    {
        moneyField.text = $"€{value}";
    }
}
