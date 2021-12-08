using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    internal int startFish;

    [SerializeField, Range(0, 60), Tooltip("Will refresh fish schooling every x frames")]
    private int fishNeighbourUpdateRate;

    [SerializeField, Range(0, 60), Tooltip("Will execute tick every x frames")]
    private int tickRate;
    
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
    }
}
