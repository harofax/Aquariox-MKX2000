using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FishType
{
    Perch,
    Goldfish,
    Mullet,
}
public class FishManager : MonoBehaviour
{
    // Yes I know... Fish is plural but I want to make it easier to distinguish 
    [SerializeField]
    private FishBase[] fishies;

    public int fishCost = 40;

    [SerializeField]
    internal FlockBehaviour[] behaviours;
    
    [SerializeField]
    private Aquarium aquarium;

    private FishType[] fishTypes;
    private Dictionary<FishType, FishBase> fishDatabase = new Dictionary<FishType, FishBase>();

    internal int fishCount;
    
    public delegate void FishChanged(int value);
    public static event FishChanged OnFishCountChanged;

    public delegate void FishCostChanged(int newCost);

    public static event FishCostChanged OnFishCostChanged;

    private static FishManager _instance;
    public static FishManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        fishTypes = (FishType[])Enum.GetValues(typeof(FishType));
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (FishBase fishy in fishies)
        {
            fishDatabase[fishy.fishType] = fishy;
        }
        
        int numStartFish = GameManager.Instance.startFish;

        for (int i = 0; i < numStartFish; i++)
        {
            FishType randomFishType = GetRandomFishType();
            SpawnNewFish(randomFishType);
        }
    }

    private FishType GetRandomFishType()
    {
        int randomFishType = Random.Range(0, fishTypes.Length);
        return fishTypes[randomFishType];
    }

    protected FishBase SpawnNewFish(FishType type)
    {
        Quaternion startRot = Random.rotation;
        Vector3 startPos = aquarium.GetRandomPosition();

        FishBase fish = Instantiate(fishDatabase[type], startPos, startRot);
        fishCount++;
        OnFishCountChanged?.Invoke(fishCount);

        return fish;
    }

    public void BuyFish()
    {
        if (GameManager.Instance.Money < fishCost)
        {
            return;
        }
        // :^)
        GameManager.Instance.addMoney(-fishCost);

        FishBaby baby = SpawnNewFish(GetRandomFishType()).AddComponent<FishBaby>();
        baby.InitBaby();
        if (fishCount % 2 == 0)
        {
            fishCost += fishCount * 2;
        }
        OnFishCostChanged?.Invoke(fishCost);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
