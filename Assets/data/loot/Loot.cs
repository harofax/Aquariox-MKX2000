using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Loot<TLootData> : MonoBehaviour where TLootData : LootData
{
    protected TLootData lootData;

    public void SetLootData(TLootData data)
    {
        lootData = data;
    }
    public abstract void Initiate();
}
public interface ILootData
{
}

public interface ILoot { }