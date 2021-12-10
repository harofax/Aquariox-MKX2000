using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Food Data")]
public class FoodData : LootData
{
    [Range(1, 10)]
    public float happinessBonus;
    [Range(10, 100)]
    public float hungerFill;
}
