using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Coin Data")]
public class CoinData : LootData
{
    public Texture coinTexture;
    public Color coinColor;
    public int coinValue;
    public float size;
}
