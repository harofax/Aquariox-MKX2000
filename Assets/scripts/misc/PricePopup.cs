using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PricePopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public GameObject priceToolTip;

    protected TMP_Text costField;

    private void Awake()
    {
        costField = priceToolTip.GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (priceToolTip != null)
        {
            priceToolTip.SetActive(true);
            priceToolTip.transform.position = eventData.position;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (priceToolTip != null)
        {
            priceToolTip.SetActive(false);
            priceToolTip.transform.position = eventData.position;
        }
    }
}
