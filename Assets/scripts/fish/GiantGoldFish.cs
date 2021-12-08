using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiantGoldFish : FishBase
{
    
    private protected override void Execute()
    {
        if (Random.Range(0, 100) < MoneyRate * HappinessModifier)
        {
            //GameObject coin = GameManager.Instance.moneyPool.GetPooledObject();
        }
    }
}
