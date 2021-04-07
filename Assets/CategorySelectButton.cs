using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CategorySelectButton : MonoBehaviour
{
    [SerializeField] private ItemCategory category;

    private Button selectButton;
    private InventoryWidget InventoryWidget;

    private void Awake()
    {
        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(OnClick);
    }

    public void Initialize(InventoryWidget inventoryWidget)
    {
        InventoryWidget = inventoryWidget;
    }

    private void OnClick()
    {
        if (!InventoryWidget) return;
        InventoryWidget.SelectCategory(category);
    }
}
