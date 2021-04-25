using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryReferencer : MonoBehaviour
{
    public static InventoryReferencer Instance;

    [SerializeField] private List<ItemScriptable> ItemList = new List<ItemScriptable>();

    private Dictionary<string, ItemScriptable> ItemDictionary = new Dictionary<string, ItemScriptable>();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        foreach(ItemScriptable itemScriptable in ItemList)
        {
            ItemDictionary.Add(itemScriptable.Name, itemScriptable);
        }
    }

    void Start()
    {
        
    }

    public ItemScriptable GetItemReference(string itemName) =>
        ItemDictionary.ContainsKey(itemName) ? ItemDictionary[itemName] : null;
}
