using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown_Project
{
    public class EnemyManager : MonoBehaviour
    {
        private Coroutine waveRoutine;

        [SerializeField] private List<GameObject> enemyPrefabs;

        [SerializeField] private List<Rect> spawnAreas;

        [SerializeField] private Color gizmocolor = new Color(1, 0, 0, 0.3f);

        private List<EnemyController> activeEnemies = new List<EnemyController>();

        private bool enemySpawnComplite;

        [SerializeField] private float timeBetweenSpawns = 0.2f;
        [SerializeField] private float timeBetweenWaves = 1f;

        private GameManager gameManager;

        internal void Init(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void StartWave(int waveCount)
        {
            if (waveCount <= 0)
            {
                gameManager.EndOfWave();
                return;
            }

            if (waveRoutine != null)
                StopCoroutine(waveRoutine);
            waveRoutine = StartCoroutine(SpawnWave(waveCount));
        }

        public void StopWave()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnWave(int waveCount)
        {
            enemySpawnComplite = false;
            yield return new WaitForSeconds(timeBetweenWaves);
            for (int i = 0; i < waveCount; i++)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
                SpawnRandomEnemy();
            }

            enemySpawnComplite = true;
        }

        private void SpawnRandomEnemy()
        {
            if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
            {
                Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
                return;
            }

            GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

            Vector2 randomPosition = new Vector2(
                Random.Range(randomArea.xMin, randomArea.xMax),
                Random.Range(randomArea.yMin, randomArea.yMax)
                );

            GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
            EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
            enemyController.Init(this, gameManager.player.transform);

            activeEnemies.Add(enemyController);
        }

        public void RemoveEnemyOnDeath(EnemyController enemy)
        {
            activeEnemies.Remove(enemy);
            if (enemySpawnComplite && activeEnemies.Count == 0)
            {
                gameManager.EndOfWave();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (spawnAreas == null) return;

            Gizmos.color = gizmocolor;
            foreach (var area in spawnAreas)
            {
                Vector3 center = new Vector3(area.x + area.width * 0.5f, area.y + area.height * 0.5f);
                Vector3 size = new Vector3(area.width, area.height);
                Gizmos.DrawCube(center, size);
            }
        }
    }
}