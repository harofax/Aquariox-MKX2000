using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPricePopup : PricePopup
{
    // Start is called before the first frame update
    void Start()
    {
        costField.text = $"€{FishManager.Instance.fishCost}";
        if (priceToolTip != null)
        {
            priceToolTip.SetActive(false);
        }
    }
}
