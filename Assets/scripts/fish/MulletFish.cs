using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulletFish : FishBase
{
    private protected override void Execute()
    {
        var lootRoll = Random.Range(0, 100);
        if (lootRoll < LootRate + LootBonus * neighbours.Count)
        {
            LootManager.Instance.SpawnDrop(this);
        }
    }

    
}
