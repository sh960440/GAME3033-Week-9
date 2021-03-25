using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace UI.Menu
{
    public class LoadGameWidget : MenuWidget
    {
        private GameDataList gameData;

        [Header("Scene To Load")]
        [SerializeField] private string sceneToLoad;

        [Header("References")]
        [SerializeField] private RectTransform loadItemsPanel;
        [SerializeField] private TMP_InputField newGameInputField;

        [Header("Prefabs")]
        [SerializeField] private GameObject SaveSlotPrefab;
        private const string SaveFileKey = "FileSaveData";
        [SerializeField] private bool debug;
        
        void Start()
        {
            if (debug)
            {
                SaveDebugData();
            }

            LoadGameData();
        }

        private void WipeChildren()
        {
            foreach (RectTransform saveSlot in loadItemsPanel)
            {
                Destroy(saveSlot.gameObject);
            }
            loadItemsPanel.DetachChildren();
        }

        private void SaveDebugData()
        {
            GameDataList dataList = new GameDataList();
            dataList.saveFileNames.AddRange(new List<string>{"Save1", "Save2", "Save3"});
            PlayerPrefs.SetString(SaveFileKey, JsonUtility.ToJson(dataList));
        }

        private void LoadGameData()
        {
            if (!PlayerPrefs.HasKey(SaveFileKey)) return;

            string jsonString = PlayerPrefs.GetString(SaveFileKey);
            gameData = JsonUtility.FromJson<GameDataList>(jsonString);

            if (gameData.saveFileNames.Count <= 0) return;

            foreach (string saveName in gameData.saveFileNames)
            {
                SaveSlotWidget widget = Instantiate(SaveSlotPrefab, loadItemsPanel).GetComponent<SaveSlotWidget>();
                //widget.SetParent(loadItemsPanel);
                widget.Initialize(this, saveName);
            }
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        public void CreateNewGame()
        {
            if (string.IsNullOrEmpty(newGameInputField.text)) return;
            GameManager.Instance.SetActiveSave(newGameInputField.text);
            LoadScene();
        }
    }

    [Serializable]
    class GameDataList
    {
        public List<string> saveFileNames = new List<string>();
    }
}

