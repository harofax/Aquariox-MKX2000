using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType
{
    Small,
    Medium,
    Large,
    Rare,
}
[System.Serializable]
struct FishCoinWeight
{
    [SerializeField]
    private FishType fishType;
    [SerializeField]
    private int[] coinWeightBonus;

    public FishCoinWeight(FishType type, int[] weights)
    {
        fishType = type;
        coinWeightBonus = weights;
    }
}
public class CoinManager : MonoBehaviour
{
    [SerializeField]
    private CoinData[] coinTypes;

    [SerializeField]
    private FishCoinWeight[] fishCoinWeights;

    private static CoinManager _instance;
    public static CoinManager Instance => _instance;
    
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

    public void SpawnCoin(FishBase fish)
    {
        int roll = Random.Range(0, 100);
        int currentIndex = 0;
        switch (fish.fishType)
        {
            case FishType.Goldfish:
                break;
            case FishType.Mullet:
                break;
            case FishType.Perch:
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
