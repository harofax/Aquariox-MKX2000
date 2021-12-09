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

    public Loot GetPooledObjectOfType(Loot dropType)
    {
        switch (dropType)
        {
            case Coin:
                return coinPool.GetPooledObject();
            case Food:
                return foodPool.GetPooledObject();
            case FishBaby:
                return fishPool.GetPooledObject();
            default:
                return null;
        }
    }
}
