using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconSlot : MonoBehaviour
{
    private ItemScriptable Item;
    private Button itemButton;
    private TMP_Text itemText;

    [SerializeField] private ItemSlotAmountWidget amountWidget;
    [SerializeField] private ItemSlotEquippedWidget equippedWidget;

    void Awake()
    {
        itemButton = GetComponent<Button>();
        itemText = GetComponentInChildren<TMP_Text>();
    }

    public void Initialize(ItemScriptable item)
    {
        Item = item;
        itemText.text = item.Name;
        amountWidget.Initialize(item);
        equippedWidget.Initialize(item);

        itemButton.onClick.AddListener(UseItem);
        Item.OnItemDestroyed += OnItemDestroyed;
    }

        public void UseItem()
        {
        Debug.Log("Use Item");
        Item.UseItem(Item.Controller)   ;
    }

    private void OnItemDestroyed()
    {
        Item = null;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (Item)
            Item.OnItemDestroyed -= OnItemDestroyed;
    }
}
