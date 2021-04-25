using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using System;

namespace Character
{
    [Serializable]
    public class WeaponSaveData : SaveDataBase
    {
        public WeaponStats WeaponStats;

        public WeaponSaveData(WeaponStats weaponStats)
        {
            Name = weaponStats.WeaponName;
            WeaponStats = weaponStats;
        }
    }
}



