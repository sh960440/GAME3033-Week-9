using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using System;
using System.Linq;
using UI.Menu;
using Enemies;

[Serializable]
public class GameSaveData
{
    public PlayerSaveData PlayerSaveData;
    public SpawnerSaveDataList SpawnerSaveDataList;

    public GameSaveData()
    {
        PlayerSaveData = new PlayerSaveData();
    }
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private GameSaveData GameSave;

    private const string SaveFileKey = "FileSaveData";

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (string.IsNullOrEmpty(GameManager.Instance.gameSaveName)) return;
        if (!PlayerPrefs.HasKey(GameManager.Instance.gameSaveName)) return;

        string jsdonString = PlayerPrefs.GetString(GameManager.Instance.gameSaveName);
        GameSave = JsonUtility.FromJson<GameSaveData>(jsdonString);
        LoadGame();
    }

    public void SaveGame()
    {
        GameSave ??= new GameSaveData();

        var savableObjects = FindObjectsOfType<MonoBehaviour>().Where(monoObject => monoObject is ISavable).ToList();

        ISavable playerSaveObject = savableObjects.First(monoObject => monoObject is PlayerController) as ISavable;
        GameSave.PlayerSaveData = (PlayerSaveData)playerSaveObject?.SaveData();

        SpawnerSaveDataList spawnerList = new SpawnerSaveDataList();
        var spawnerDataList = savableObjects.OfType<ZombieSpawner>();
        foreach (ZombieSpawner spawner in spawnerDataList)
        {
            ISavable saveObject = spawner.GetComponent<ISavable>();
            spawnerList.SpawnerData.Add(saveObject?.SaveData() as SpawnerSaveData);
        }

        GameSave.SpawnerSaveDataList = spawnerList;

        string jsonString = JsonUtility.ToJson(GameSave);
        PlayerPrefs.SetString(GameManager.Instance.gameSaveName, jsonString);

        SaveToGameSaveList();
    }

    private void SaveToGameSaveList()
    {
        if (PlayerPrefs.HasKey(SaveFileKey))
        {
            GameDataList saveList = JsonUtility.FromJson<GameDataList>(PlayerPrefs.GetString(SaveFileKey));
            
            if (saveList.saveFileNames.Contains(GameManager.Instance.gameSaveName)) return;
            saveList.saveFileNames.Add(GameManager.Instance.gameSaveName);

            PlayerPrefs.SetString(SaveFileKey, JsonUtility.ToJson(saveList));
        }
        else
        {
            GameDataList saveList = new GameDataList();
            saveList.saveFileNames.Add(GameManager.Instance.gameSaveName);

            PlayerPrefs.SetString(SaveFileKey, JsonUtility.ToJson(saveList));
        }
    }

    public void LoadGame()
    {
        var savableObjects = FindObjectsOfType<MonoBehaviour>().Where(monoObject => monoObject is ISavable).ToList();

        ISavable playerObject = savableObjects.First(monoObject => monoObject is PlayerController) as ISavable;
        playerObject?.LoadData(GameSave.PlayerSaveData);

        foreach (SpawnerSaveData spawnerData in GameSave.SpawnerSaveDataList.SpawnerData)
        {
            ISavable saveObject = savableObjects.Find(savableObject => spawnerData.Name == savableObject.name) as ISavable;
            saveObject?.LoadData(spawnerData);
        }
    }
}