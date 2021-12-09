using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : Loot
{
    [SerializeField]
    public CoinData coinData;

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

    public override void Initiate()
    {
        propBlock.SetColor(matColorID, coinData.coinColor);
        propBlock.SetTexture(matTextureID, coinData.coinTexture);
        coinRenderer.SetPropertyBlock(propBlock);

        transform.localScale *= coinData.size;
        value = coinData.coinValue;
    }
}
