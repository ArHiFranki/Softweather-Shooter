using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Softweather.Spawner
{
    public class Spawner : ObjectPool
    {
        [SerializeField] private int enemyToSpawn;
        [SerializeField] private List<Transform> spawnPoints;

        private void Start()
        {
            InitPool();
            StartCoroutine(SpawnWave());
        }

        private IEnumerator SpawnWave()
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
            Transform currentPoint = spawnPoints[index];

            if (spawnPoints.Contains(currentPoint))
            {
                spawnPoints.Remove(currentPoint);
            }

            return currentPoint.transform.position;
        }

        private void SetObject(GameObject spawnObject, Vector3 spawnPoint)
        {
            spawnObject.SetActive(true);
            spawnObject.transform.position = spawnPoint;
        }
    }
}