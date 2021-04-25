using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Character
{
    [Serializable]
    public class PlayerSaveData : SaveDataBase
    {
        public float CurrentHealth; 
        public Vector3 Position;
        public Quaternion Rotation;
        public WeaponSaveData EquippedWeaponData;
        public List<ItemSaveData> ItemList;
    }
    
}