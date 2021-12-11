using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum DropRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Loot/Drop Table")]
public class DropTable : ScriptableObject
{
    public int normalDropRate;
    public int rareDropRate;
    public int epicDropRate;
    public int legendaryDropRate;

    // [HideInInspector]
    // public int total;

    public LootContainer[] drops;
    
    private void OnEnable()
    {
        //total = normalDropRate + rareDropRate + epicDropRate + legendaryDropRate;
    }

    public LootData GetRandomDrop()
    {
        int roll = Random.Range(0, legendaryDropRate);
        DropRarity dropRarity;
        
        switch (roll)
        {
            case var n when (n <= normalDropRate):
                dropRarity = DropRarity.Common;
                break;
            case var n when (n > normalDropRate && n <= rareDropRate):
                dropRarity = DropRarity.Rare;
                break;
            case var n when (n > rareDropRate && n <= epicDropRate):
                dropRarity = DropRarity.Epic;
                break;
            case var n when (n > epicDropRate && n <= legendaryDropRate):
                dropRarity = DropRarity.Legendary;
                break;
            default:
                Debug.LogError("error not loot good", this);
                dropRarity = DropRarity.Common;
                break;
        }

        return drops[(int)dropRarity].GetRandomDrop();
    }
}
