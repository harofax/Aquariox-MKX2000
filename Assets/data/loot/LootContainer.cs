using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Loot Tier")]
public class LootContainer : ScriptableObject
{
    [SerializeField]
    private LootData[] drops;

    public LootData GetRandomDrop()
    {
        return drops[Random.Range(0, drops.Length)];
    }
}
