using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace Enemies
{
    [RequireComponent(typeof(SpawnerStateMachine))]
    public class ZombieSpawner : MonoBehaviour, ISavable
    {
        public delegate void WaveComplete(SpawnerStateEnum CurrentState);

        public event WaveComplete OnWaveComplete;

        private bool Initialized = false;

        [SerializeField] private int numberOfZombiesToSpawn;

        public GameObject[] zombiePrefab;
        public SpawnerVolume[] spawnerVolumes;

        public GameObject TargetObject => followGameObject;
        private GameObject followGameObject;

        private SpawnerStateMachine StateMachine;

        private SpawnerStateEnum StartingState;

        private void Awake()
        {
            StateMachine = GetComponent<SpawnerStateMachine>();
            followGameObject = GameObject.FindGameObjectWithTag("Player");
        }

        void Start()
        {
            ZombieWaveSpawnerState beginnerWave = new ZombieWaveSpawnerState(this, StateMachine)
            {
                ZombiesToSpawn = 5,
                NextState = SpawnerStateEnum.Intermediate
            };
            StateMachine.AddState(SpawnerStateEnum.Beginner, beginnerWave);

            ZombieWaveSpawnerState intermediateWave = new ZombieWaveSpawnerState(this, StateMachine)
            {
                ZombiesToSpawn = 10,
                NextState = SpawnerStateEnum.Complete
            };
            StateMachine.AddState(SpawnerStateEnum.Intermediate, intermediateWave);

            
            StateMachine.Initialize(StartingState);
        }

        public void CompleteWave(SpawnerStateEnum nextState)
        {
            OnWaveComplete?.Invoke(nextState);
        }

        public SaveDataBase SaveData()
        {
            SpawnerSaveData saveData = new SpawnerSaveData
            {
                Name = gameObject.name,
                CurrentState = StateMachine.ActiveEnumState
            };

            return saveData;
        }

        public void LoadData(SaveDataBase saveData)
        {
            SpawnerSaveData spawnerSaveData = (SpawnerSaveData) saveData;
            //StateMachine.Initialize(spawnerSaveData.CurrentState);
            //Initialized = true;
            StartingState = spawnerSaveData.CurrentState;
        }
    }

    [Serializable]
    public class SpawnerSaveData : SaveDataBase
    {
        public SpawnerStateEnum CurrentState;
    }
}

