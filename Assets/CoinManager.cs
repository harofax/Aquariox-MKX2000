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
    private int coinWeightBonus;

    public FishCoinWeight(FishType type, int bonus)
    {
        fishType = type;
        coinWeightBonus = bonus;
    }
}
public class CoinManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPool coinPool;
    
    [SerializeField]
    private CoinData[] coinTypes;

    [SerializeField]
    private FishCoinWeight[] fishCoinWeights;
    
    [SerializeField]
    private CoinDropTable dropTable;

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
        int roll = Random.Range(0, dropTable.total);
        var coin = coinPool.GetPooledObject();
        
        
        switch (roll)
        {
            case int n when (n < dropTable.smallCoinRange):
                coin.GetComponent<Coin>().SetCoinData(coinTypes[0]);
                break;
            case int n when (n > dropTable.smallCoinRange && n < dropTable.MediumCoinRange):
                coin.GetComponent<Coin>().SetCoinData(coinTypes[1]);
                break;
            case int n when (n > dropTable.MediumCoinRange && n < dropTable.LargeCoinRange):
                coin.GetComponent<Coin>().SetCoinData(coinTypes[2]);
                break;
            case int n when (n > dropTable.LargeCoinRange && n < dropTable.RareCoinRange):
                coin.GetComponent<Coin>().SetCoinData(coinTypes[3]);
                break;
            default:
                break;
        }

        coin.transform.position = fish.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
