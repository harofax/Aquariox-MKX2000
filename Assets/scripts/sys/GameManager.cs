using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    internal int startFish;

    [SerializeField, Range(0, 60), Tooltip("Will refresh fish schooling every x frames")]
    private int fishNeighbourUpdateRate;

    [SerializeField, Range(0, 60), Tooltip("Will execute tick every x frames")]
    private int tickRate;


    private int money;
    public int Money => money;

    public delegate void MoneyChanged(int value);
    public static event MoneyChanged OnMoneyChanged;

    public delegate void FishSchoolTick();
    public static event FishSchoolTick OnSchoolingTick;

    public delegate void Tick();
    public static event Tick OnTick;
    
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
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
        LoadGame();
    }

    private void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedMoney"))
        {
            money = PlayerPrefs.GetInt("SavedMoney");
        }

        if (PlayerPrefs.HasKey("SavedFish"))
        {
            startFish = PlayerPrefs.GetInt("SavedFish");
        }
    }

    private void OnDestroy()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("SavedMoney", money);
        PlayerPrefs.SetInt("SavedFish", FishManager.Instance.fishCount);
        PlayerPrefs.Save();
    }

    public void addMoney(int value)
    {
        money += value;
        OnMoneyChanged?.Invoke(money);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % tickRate == 0)
        {
            OnTick?.Invoke();
        }

        if (Time.frameCount % fishNeighbourUpdateRate == 0)
        {
            OnSchoolingTick?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveGame();
            Application.Quit();
        }
    }
}
