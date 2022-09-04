using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Softweather.Enemy;

namespace Softweather.ObjectSpawner
{
    public class Spawner : ObjectPool
    {
        [SerializeField] private int startEnemiesCount;
        [SerializeField] private int spawnPerDeathCount;
        [SerializeField] private List<Transform> spawnPoints;

        private Transform previousSpawnPoint;

        private void Start()
        {
            InitPool();
            StartCoroutine(SpawnWave(startEnemiesCount));
        }

        public void SpawnAdditinalEnemies()
        {
            StartCoroutine(SpawnWave(spawnPerDeathCount));
        }

        private IEnumerator SpawnWave(int enemyToSpawn)
        {
            for (int i = 0; i < enemyToSpawn; i++)
            {
                TryCreateObjectFromPool(enemyPool);
            }
            yield return null;
        }

        private void TryCreateObjectFromPool(List<GameObject> objectPool)
        {
            if (TryGetObject(objectPool, out GameObject objectFromPool))
            {
                SetObject(objectFromPool, GenerateSpawnPointPosition());
            }
        }

        private Vector3 GenerateSpawnPointPosition()
        {
            int index = Random.Range(0, spawnPoints.Count);
            Transform currentSpawnPoint = spawnPoints[index];

            if (currentSpawnPoint == previousSpawnPoint)
            {
                while (currentSpawnPoint == previousSpawnPoint)
                {
                    index = Random.Range(0, spawnPoints.Count);
                    currentSpawnPoint = spawnPoints[index];
                }
            }
            else
            {
                previousSpawnPoint = currentSpawnPoint;
            }

            return currentSpawnPoint.transform.position;
        }

        private void SetObject(GameObject spawnObject, Vector3 spawnPoint)
        {
            if (spawnObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.InitSpawner(this);
                enemyHealth.ResetEnemy();
            }
            spawnObject.SetActive(true);
            spawnObject.transform.position = spawnPoint;
        }
    }
}