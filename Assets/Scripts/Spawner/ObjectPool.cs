using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Softweather.Enemy;
using Softweather.Core;

namespace Softweather.Spawner
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject enemyContainer;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemyPoolCapacity;

        //[SerializeField] protected DisplayLeaderboard leaderboard;
        [SerializeField] protected Transform playerTransform;
        [SerializeField] protected GameController myGameController;

        protected List<GameObject> enemyPool = new List<GameObject>();

        private void Initialize(GameObject prefab, List<GameObject> pool, GameObject container, int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                GameObject spawned = Instantiate(prefab, container.transform);
                spawned.SetActive(false);
                //spawned.GetComponent<Health>().InitLeaderboard(leaderboard);

                // ��� ������� ��� ������ ����������
                if (spawned.TryGetComponent(out EnemyAI enemyAI))
                {
                    enemyAI.InitTarget(playerTransform);
                }

                if (spawned.TryGetComponent(out InitializeGameController gameController))
                {
                    gameController.InitGameController(myGameController);
                }

                pool.Add(spawned);
            }
        }

        protected void InitPool()
        {
            Initialize(enemyPrefab, enemyPool, enemyContainer, enemyPoolCapacity);
        }

        protected bool TryGetObject(List<GameObject> pool, out GameObject result)
        {
            result = pool.FirstOrDefault(p => p.activeSelf == false);
            return result != null;
        }
    }
}