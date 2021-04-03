using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptable : EquippableScriptable
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController controller)
    {
        if (Equipped)
        {
            controller.WeaponHolder.UnEquipItem();
        }
        else
        {
            controller.WeaponHolder.EquipWeapon(this);
        }

        base.UseItem(controller);
    }
}
