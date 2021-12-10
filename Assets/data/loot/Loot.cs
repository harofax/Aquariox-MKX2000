using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    private Vector3 defaultScale;

    public void SetDefaultScale(Vector3 scale)
    {
        defaultScale = scale;
    }

    public abstract void SetLootData(LootData data);
    public abstract void Initiate();

    public void ReturnToPool()
    {
        this.gameObject.SetActive(false);
        transform.localScale = defaultScale;
    }
}
public interface ILootData
{
}

public interface ILoot { }