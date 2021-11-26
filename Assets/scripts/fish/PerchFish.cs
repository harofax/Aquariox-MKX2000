using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerchFish : FishBase
{
    [SerializeField]
    private FishData abborreData;
    
    // Start is called before the first frame update
    void Awake()
    {
        SetFishData(abborreData);
    }

    private protected override void Execute()
    {
        var hunger = HungerRate;
    }
}
