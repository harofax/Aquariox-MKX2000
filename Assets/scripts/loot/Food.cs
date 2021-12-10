using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Loot
{
    private FoodData foodData;

    private float happinessBonus;
    private float hungerFill;

    public override void SetLootData(LootData data)
    {
        foodData = (FoodData)data;
    }
    
    public override void Initiate()
    {
        happinessBonus = foodData.happinessBonus;
        hungerFill = foodData.hungerFill;
    }
}
