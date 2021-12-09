using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/CoinDropTable")]
public class CoinDropTable : ScriptableObject
{
    public int smallCoinRange;
    public int MediumCoinRange;
    public int LargeCoinRange;
    public int RareCoinRange;

    public int total;

    private void OnEnable()
    {
        total = smallCoinRange + MediumCoinRange + LargeCoinRange + RareCoinRange;
    }
}
