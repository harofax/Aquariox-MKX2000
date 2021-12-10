using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTaxi : MonoBehaviour
{
    [SerializeField]
    private ObjectPool<Coin> coinPool;

    [SerializeField]
    private ObjectPool<FishBaby> fishPool;

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

    public GameObject GetPooledObjectOfType(LootData dropType)
    {
        switch (dropType)
        {
            case CoinData:
                return coinPool.GetPooledObject().gameObject;
            case FoodData:
                return foodPool.GetPooledObject().gameObject;
            case FishData:
                return fishPool.GetPooledObject().gameObject;
            default:
                return null;
        }
    }

    public Coin GetPooledCoin()
    {
        return coinPool.GetPooledObject();
    }

    public Food GetPooledFood()
    {
        return foodPool.GetPooledObject();
    }

    public FishBaby GetPooledFish()
    {
        return fishPool.GetPooledObject();
    }
}
