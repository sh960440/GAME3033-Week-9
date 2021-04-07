using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using UnityEngine;

public class InventoryWidget : GameHUDWidget
{
    private ItemDisplayPanel itemDisplayPanel;
    private List<CategorySelectButton> categoryButton;

    private PlayerController playerController;
    
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        categoryButton = GetComponentsInChildren<CategorySelectButton>().ToList();
        itemDisplayPanel = GetComponentInChildren<ItemDisplayPanel>();
        foreach (CategorySelectButton button in categoryButton)
        {
            button.Initialize(this);
        }
    }

    private void OnEnable()
    {
        if (!playerController || !playerController.Inventory) return;
        if (playerController.Inventory.GetItemCount() <= 0) return;

        itemDisplayPanel.PopulatePanel(playerController.Inventory.GetItemsOfCategory(ItemCategory.NONE));
    }

    public void SelectCategory(ItemCategory category)
    {
        if (!playerController || !playerController.Inventory) return;
        if (playerController.Inventory.GetItemCount() <= 0) return;

        itemDisplayPanel.PopulatePanel(playerController.Inventory.GetItemsOfCategory(category));
    }
}
