using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;

namespace AncientAliens.TileObjects
{
    public class BarbarianSpawner : MonoBehaviour
    {
        [SerializeField] GameObject barbarian;

        [SerializeField] AnimationCurve spawnRateCurve;
        [SerializeField] float spawnRate = 20;
        [SerializeField] float depreciationRate = 1.1f;
        [SerializeField] float minSpawnRate = 6;
        [SerializeField] private float currentSpawnRate;
        private bool started;

        private void Awake()
        {
            GameManager.Instance.onGameStarted += BeginSpawning;
        }

        private void OnDestroy()
        {
            StopAllCoroutines(); // unsure if this is needed
        }

        private void BeginSpawning()
        {
            started = true;            
            currentSpawnRate = spawnRate;
            StartCoroutine(IncreaseSpawnRate());
            StartCoroutine(SpawnBarbariansOverTime());

        }

        private void SpawnBarbarian()
        {
            var randomSide = Random.Range(0, 4);
            var spawnIndex = Vector2.zero;

            var xMax = EasyGrid.SizeX;
            var yMax = EasyGrid.SizeY;

            switch(randomSide)
            {
                case 0:
                    spawnIndex = new Vector2(0, Random.Range(0, yMax));
                    break;
                case 1:
                    spawnIndex = new Vector2(xMax-1, Random.Range(0, yMax));
                    break;
                case 2:
                    spawnIndex = new Vector2(Random.Range(0, xMax), 0);
                    break;
                case 3:
                    spawnIndex = new Vector2(Random.Range(0, xMax), yMax-1);
                    break;
            }

            if (!EasyGrid.GetTileAt(spawnIndex).IsEmpty()) return; //Edge case: stop if something in that tile

            var tileObject = Instantiate(barbarian);
            EasyGrid.AssignTileObjectToTile(tileObject, (int)spawnIndex.x, (int)spawnIndex.y);

        }

        IEnumerator IncreaseSpawnRate()
        {

            while(currentSpawnRate > minSpawnRate)
            {

                yield return new WaitForSeconds(20);

                currentSpawnRate /= depreciationRate;

                
                

            }

            currentSpawnRate = minSpawnRate;

        }

        IEnumerator SpawnBarbariansOverTime()
        {


            while (true)
            {

                if (GameManager.Instance.GamePaused)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                yield return new WaitForSeconds(currentSpawnRate);

                if (GameManager.Instance.GamePaused)
                {
                    continue;
                }

                SpawnBarbarian();
            }
        }

    }
}
