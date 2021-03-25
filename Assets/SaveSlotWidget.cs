using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Menu;
using TMPro;

public class SaveSlotWidget : MonoBehaviour
{
    private string SaveName;

    private GameManager manager;
    private LoadGameWidget loadWidget;

    [SerializeField] private TMP_Text saveNameText;

    void Awake()
    {
        manager = GameManager.Instance;
    }

    public void Initialize(LoadGameWidget parentWidget, string saveName)
    {
        loadWidget = parentWidget;
        SaveName = saveName;
        saveNameText.text = saveName;
    }

    public void SelectSave()
    {
        manager.SetActiveSave(SaveName);
        loadWidget.LoadScene();
    }
}
