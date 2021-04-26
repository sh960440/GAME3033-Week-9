using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using UnityEngine.SceneManagement;

public class ZombieGameMode : MonoBehaviour
{
    private ZombieSpawner Spawner;

    public void Awake()
    {
        Spawner = FindObjectOfType<ZombieSpawner>();
        Spawner.OnWaveComplete += OnWaveComplete;
    }

    private void OnWaveComplete(SpawnerStateEnum stateEnum)
    {
        if (stateEnum == SpawnerStateEnum.Complete)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("MenuScene");
        } 
    }
}
