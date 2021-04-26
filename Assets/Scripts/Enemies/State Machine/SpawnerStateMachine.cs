using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health;

namespace Enemies
{
    public enum SpawnerStateEnum
    {
        Beginner,
        Intermediate,
        Hard,
        Complete
    }
    public class SpawnerStateMachine : StateMachine<SpawnerStateEnum>
    {

    }

    public class SpawnerState : State<SpawnerStateEnum>
    {
        protected ZombieSpawner Spawner;
        public SpawnerState(ZombieSpawner spawner, SpawnerStateMachine stateMachine) : base(stateMachine)
        {
            Spawner = spawner;
        }

        protected void SpawnZombie()
        {
            GameObject zombieToSpawn = Spawner.zombiePrefab[Random.Range(0, Spawner.zombiePrefab.Length)];
            SpawnerVolume spawnerVolume = Spawner.spawnerVolumes[Random.Range(0, Spawner.spawnerVolumes.Length)];
            
            if (Spawner.TargetObject)
            {
                GameObject zombie = Object.Instantiate(zombieToSpawn, spawnerVolume.GetPositionInBounds(), spawnerVolume.transform.rotation);

                zombie.GetComponent<ZombieComponent>().Initialize(Spawner.TargetObject);
                zombie.GetComponent<HealthComponent>().OnDeath += OnZombieDeath;
            }
        }

        protected virtual void OnZombieDeath()
        {
            
        }
    }

    class ZombieWaveSpawnerState : SpawnerState
    {
        public int ZombiesToSpawn = 5;
        public SpawnerStateEnum NextState = SpawnerStateEnum.Intermediate;

        private int TotalZombiesKilled = 0;
        public ZombieWaveSpawnerState(ZombieSpawner spawner, SpawnerStateMachine stateMachine) : base(spawner, stateMachine)
        {

        }

        public override void Start()
        {
            base.Start();

            for (int i = 0; i < ZombiesToSpawn; i++)
            {
                SpawnZombie();
            }
        }

        protected override void OnZombieDeath()
        {
            base.OnZombieDeath();
            Debug.Log("Zombie killed");

            TotalZombiesKilled++;

            if (TotalZombiesKilled < ZombiesToSpawn) return;

            StateMachine.ChanceState(NextState);
            Spawner.CompleteWave(NextState);
        }
    } 
}


