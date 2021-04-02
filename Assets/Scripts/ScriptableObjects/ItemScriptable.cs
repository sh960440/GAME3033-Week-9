using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    NONE,
    WEAPON,
    CONSUMABLE,
    EQUIPMENT,
    AMMO
}

public class ItemScriptable : ScriptableObject
{
    public string name = "Item";
    public ItemCategory itemCategory;

}
