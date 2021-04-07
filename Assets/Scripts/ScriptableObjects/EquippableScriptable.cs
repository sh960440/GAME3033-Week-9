using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Weapons;

public abstract class EquippableScriptable : ItemScriptable
{
    public bool Equipped
    {
        get => m_Equipped;
        set
        {
            m_Equipped = value;
            OnEquipStatusChange?.Invoke();
        }
    }
    private bool m_Equipped = false;

    public delegate void EquipStatusChange();
    public event EquipStatusChange OnEquipStatusChange;

    public override void UseItem(PlayerController controller)
    {
        m_Equipped = !m_Equipped;
    }
}


