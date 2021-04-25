using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSlotAmountWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text amountText;
    private ItemScriptable Item;

    private void Awake()
    {
        HideWidget();
    }

    public void ShowWidget()
    {
        gameObject.SetActive(true);
    }

    public void HideWidget()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(ItemScriptable item)
    {
        if (!item.stackable) return;

        Item = item;
        ShowWidget();
        Item.OnAmountChange += OnAmountChange;
        OnAmountChange();
    }

    private void OnAmountChange()
    {
        amountText.text = Item.Amount.ToString();
    }

    private void OnDisable()
    {
        if (Item)
            Item.OnAmountChange -= OnAmountChange;
    }
}
