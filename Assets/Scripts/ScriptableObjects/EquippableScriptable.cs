using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Weapons;

public abstract class EquippableScriptable : ItemScriptable
{
    public bool Equipped
    {
        get => m_equipped;
        set
        {
            m_equipped = value;
        }
    }
    private bool m_equipped;

    public override void UseItem(PlayerController controller)
    {
        m_equipped = !m_equipped;
    }
}


