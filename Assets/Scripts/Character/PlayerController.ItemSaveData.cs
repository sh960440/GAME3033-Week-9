using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Character
{
    [Serializable]
    public class ItemSaveData : SaveDataBase
    {
        public int Amount;

        public ItemSaveData(ItemScriptable item)
        {
            Name = item.Name;
            Amount = item.Amount;
        }
    }
}