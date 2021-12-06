using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FishType
{
    Perch,
    Goldfish,
    Mullet
}
public class FishManager : MonoBehaviour
{
    // Yes I know... Fish is plural but I want to make it easier to distinguish 
    [SerializeField]
    private FishBase[] fishies;


    [SerializeField]
    internal FlockBehaviour[] behaviours;
    
    [SerializeField]
    private Aquarium aquarium;

    private FishType[] fishTypes;
    private Dictionary<FishType, FishBase> fishDatabase = new Dictionary<FishType, FishBase>();


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
            // int fishType = Random.Range(0, fishies.Length);
            //
            // Quaternion startRot = Quaternion.identity;
            //
            // // coinflip to rotate the other way
            // startRot.eulerAngles = Random.Range(0, 2) == 0 ? 
            //     startRot.eulerAngles + 180f * Vector3.up :
            //     startRot.eulerAngles;
            //
            // startRot.eulerAngles = Random.insideUnitCircle;
            //
            // Vector3 pos = aquarium.GetRandomPosition();
            //
            // FishBase fish = Instantiate(fishies[fishType], pos, startRot);
        }
    }

    private FishType GetRandomFishType()
    {
        int randomFishType = Random.Range(0, fishTypes.Length);
        return fishTypes[randomFishType];
    }

    protected void SpawnNewFish(FishType type)
    {
        Quaternion startRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        Vector3 startPos = aquarium.GetRandomPosition();

        FishBase fish = Instantiate(fishDatabase[type], startPos, startRot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
