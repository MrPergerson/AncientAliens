using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;

namespace AncientAliens
{
    public class BarbarianSpawner : MonoBehaviour
    {
        [SerializeField] GameObject barbarian;

        [SerializeField] int spawnRate = 10;

        private void Start()
        {
            StartCoroutine(SpawnBarbariansOverTime());
        }

        private void OnDestroy()
        {
            StopAllCoroutines(); // unsure if this is needed
        }

        private Vector2 GetRandomAdjacentWonderTile()
        {
            return GameManager.Instance.AdjacentWonderTiles[Random.Range(0, 12)].index;
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

            var pathMovement = tileObject.GetComponent<PathMovement>();
            pathMovement.GetPathTo(EasyGrid.GetTileAt(GetRandomAdjacentWonderTile()));

        }

        IEnumerator SpawnBarbariansOverTime()
        {
            while(true)
            {


                if (GameManager.Instance.GamePaused)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                yield return new WaitForSeconds(spawnRate);

                if (GameManager.Instance.GamePaused)
                {
                    continue;
                }

                SpawnBarbarian();
            }
        }

    }
}
