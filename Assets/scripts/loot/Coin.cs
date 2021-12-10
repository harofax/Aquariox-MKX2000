using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : Loot<CoinData>, ILoot
{
    private CoinData coinData;
    private int value;
    public int Value => value;
    
    private MaterialPropertyBlock propBlock;
    private Renderer coinRenderer;
    private static readonly int matColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int matTextureID = Shader.PropertyToID("_BaseColorMap");
    
    private void Start()
    {
        coinRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void SetLootData(ILootData data)
    {
        coinData = (CoinData)data;
    }

    public override void Initiate()
    {
        propBlock.SetColor(matColorID, lootData.coinColor);
        propBlock.SetTexture(matTextureID, lootData.coinTexture);
        coinRenderer.SetPropertyBlock(propBlock);

        transform.localScale *= lootData.size;
        value = lootData.coinValue;
    }
}
