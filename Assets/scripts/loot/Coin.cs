using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : Loot
{
    [SerializeField]
    private Renderer coinRenderer;
    
    private CoinData coinData;
    private int value;
    public int Value => value;
    
    private MaterialPropertyBlock propBlock;
    private static readonly int matColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int matTextureID = Shader.PropertyToID("_BaseColorMap");
    
    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
    }

    public override void SetLootData(LootData data)
    {
        coinData = (CoinData)data;
    }

    public override void Initiate()
    {
        propBlock.SetColor(matColorID, coinData.coinColor);
        propBlock.SetTexture(matTextureID, coinData.coinTexture);
        coinRenderer.SetPropertyBlock(propBlock);

        transform.localScale *= coinData.size;
        value = coinData.coinValue;
    }
}
