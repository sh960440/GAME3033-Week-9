using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private List<ItemScriptable> Items = new List<ItemScriptable>();

    private PlayerController controller;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public List<ItemScriptable> GetItemList() => Items;

    public int GetItemCount() => Items.Count;

    public ItemScriptable FindItem(string itemName)
    {
        return Items.Find((invItem) => invItem.Name == itemName);
    }

    public void AddItem(ItemScriptable item, int amount = 0)
    {
        int itemIndex = Items.FindIndex(itemScript => itemScript.Name == item.Name);
        if (itemIndex != -1)
        {
            ItemScriptable listItem = Items[itemIndex];
            if (listItem.stackable && listItem.Amount < listItem.maxStack)
            {
                listItem.ChangeAmount(item.Amount);
            }
        }
        else
        {
            if (item == null) return;

            ItemScriptable itemClone = Instantiate(item);
            itemClone.Initialize(controller);
            itemClone.SetAmount(amount <= 1 ? item.Amount : amount);
            Items.Add(itemClone);
        }
    }

    public void DeleteItem(ItemScriptable item)
    {
        int itemIndex = Items.FindIndex(listItem => listItem.Name == item.Name);
        if (itemIndex == -1) return;

        Items.Remove(item);
    }

    public List<ItemScriptable> GetItemsOfCategory(ItemCategory itemCategory)
    {
        if (Items == null || Items.Count <= 0) return null;
        if (itemCategory == ItemCategory.NONE) return Items;

        return Items.FindAll(item => item.itemCategory == itemCategory);
    }
}
