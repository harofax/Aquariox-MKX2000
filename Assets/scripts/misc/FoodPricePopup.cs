using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPricePopup : PricePopup
{
    void Start()
    {
        costField.text = $"€{LootManager.Instance.buyFoodCost}";
        if (priceToolTip != null)
        {
            priceToolTip.SetActive(false);
        }
    }
}
