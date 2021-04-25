using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEquippedWidget : MonoBehaviour
{
    private EquippableScriptable equippable;
    [SerializeField] private Image enableImage;

    private void Awake()
    {
        HideWidget();
    }

    public void ShowWidget()
    {
        gameObject.SetActive(true);
    }

    public void HideWidget()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(ItemScriptable item)
    {
        if (!(item is EquippableScriptable equItem)) return;

        equippable = equItem;
        ShowWidget();
        equippable.OnEquipStatusChange += OnEquipmentChange;
        OnEquipmentChange();
    }

    private void OnEquipmentChange()
    {
        enableImage.gameObject.SetActive(equippable.Equipped);
    }

    private void OnDisable()
    {
        if (equippable)
            equippable.OnEquipStatusChange -= OnEquipmentChange;
    }
}
