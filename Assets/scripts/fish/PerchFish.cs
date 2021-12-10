using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerchFish : FishBase
{
    private protected override void Execute()
    {
        var lootRoll = Random.Range(0, 100);
        if (lootRoll < LootRate + LootBonus * fishbody.velocity.magnitude)
        {
            LootManager.Instance.SpawnDrop(this);
        }
    }
    
}
