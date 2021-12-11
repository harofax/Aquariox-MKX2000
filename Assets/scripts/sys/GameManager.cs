using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private int numFish;


    public int Money
    {
        get => money;
        private set
        {
            if (value != money)
            {
                money = value;
                OnMoneyChanged?.Invoke(money);
            } 
        } 
    }

    public delegate void MoneyChanged(int value);
    public static event MoneyChanged OnMoneyChanged;

    public delegate void FishSchoolTick();
    public static event FishSchoolTick OnSchoolingTick;

    public delegate void Tick();
    public static event Tick OnTick;

    public delegate void SlowTick();
    public static event SlowTick OnSlowTick;
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private const string saveMoneyPath = "moneySaved";
    private const string saveFishNumPath = "numberOfFish";
    

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
        LoadGame();
    }

    private void OnEnable()
    {
        FishManager.OnFishCountChanged += RefreshFish;
    }

    private void OnDisable()
    {
        FishManager.OnFishCountChanged -= RefreshFish;
    }

    private void RefreshFish(int value)
    {
        numFish = value;
    }

    private void LoadGame()
    {
        if (PlayerPrefs.HasKey(saveMoneyPath))
        {
            Money = PlayerPrefs.GetInt(saveMoneyPath, 0);
            OnMoneyChanged?.Invoke(money);
        }

        if (PlayerPrefs.HasKey(saveFishNumPath))
        {
            startFish = PlayerPrefs.GetInt(saveFishNumPath, startFish);
            numFish = startFish;
        }
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt(saveMoneyPath, Money);
        PlayerPrefs.SetInt(saveFishNumPath, numFish);
        PlayerPrefs.Save();
    }

    public void addMoney(int value)
    {
        Money += value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % tickRate == 0)
        {
            OnTick?.Invoke();
        }

        if (Time.frameCount % tickRate * 2 == 0)
        {
            OnSlowTick?.Invoke();
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
