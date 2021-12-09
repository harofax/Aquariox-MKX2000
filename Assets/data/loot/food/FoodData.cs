using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Food Data")]
public class FoodData : ScriptableObject
{
    public float happinessBonus;
    public float babyProgress;
    public Mesh foodMesh;
}
