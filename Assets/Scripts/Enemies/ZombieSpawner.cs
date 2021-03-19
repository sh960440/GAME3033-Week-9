using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private int numberOfZombiesToSpawn;

        [SerializeField] private GameObject[] zombiePrefab;
        [SerializeField] private SpawnerVolume[] spawnerVolumes;

        private GameObject followGameObject;

        // Start is called before the first frame update
        void Start()
        {
            followGameObject = GameObject.FindGameObjectWithTag("Player");

            for (int index = 0; index < numberOfZombiesToSpawn; index++)
            {
                SpawnZombie();
            }
        }

        void SpawnZombie()
        {
            GameObject zombieToSpawn = zombiePrefab[Random.Range(0, zombiePrefab.Length)];
            SpawnerVolume spawnerVolume = spawnerVolumes[Random.Range(0, spawnerVolumes.Length)];
            
            if (followGameObject)
            {
                GameObject zombie = Instantiate(zombieToSpawn, spawnerVolume.GetPositionInBounds(), spawnerVolume.transform.rotation);

                zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
            }
        }
    }
}

