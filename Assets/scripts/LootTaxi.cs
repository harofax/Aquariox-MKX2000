using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTaxi : MonoBehaviour
{
    [SerializeField]
    private ObjectPool<Coin> coinPool;

    [SerializeField]
    private ObjectPool<Food> foodPool;


    private static LootTaxi _instance;
    public static LootTaxi Instance => _instance;
    
    // Start is called before the first frame update
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

    private void Start()
    {
        SetDefaultScales(coinPool);
        SetDefaultScales(foodPool);
    }

    private void SetDefaultScales<T>(ObjectPool<T> OPool) where T : Loot
    {
        foreach (var lootItem in OPool.pool)
        {
            lootItem.SetDefaultScale(lootItem.transform.localScale);
        }
    }

    public Loot GetPooledLootOfType(LootData dropType)
    {
        return dropType switch
        {
            CoinData => coinPool.GetPooledObject(),
            FoodData => foodPool.GetPooledObject(),
            _ => null
        };
    }

    public Coin GetPooledCoin()
    {
        return coinPool.GetPooledObject();
    }

    public Food GetPooledFood()
    {
        return foodPool.GetPooledObject();
    }
}
