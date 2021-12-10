using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Loot<FoodData>, ILoot
{
    // [SerializeField]
    private FoodData foodData;

    private float happinessBonus;
    private float hungerFill;

    public void SetLootData(FoodData data)
    {
        foodData = data;
    }

    public override void Initiate()
    {
        happinessBonus = lootData.happinessBonus;
        hungerFill = lootData.hungerFill;
    }
}
