using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiantGoldFish : FishBase
{
    
    
    private protected override void Execute()
    {
        var lootRoll = Random.Range(0, 100);
        if (lootRoll < LootRate + LootBonus * HappinessModifier * (CurrentHunger / 50))
        {
            LootManager.Instance.SpawnDrop(this);
        }
    }
}
