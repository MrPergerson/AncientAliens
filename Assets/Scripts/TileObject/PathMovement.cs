using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;

namespace AncientAliens.TileObjects
{
    [RequireComponent(typeof(Pathfinding), typeof(TileObject))]
    public class PathMovement : MonoBehaviour
    {
        private Pathfinding pathfinding;
        List<Tile> pathToTarget;
        [SerializeField] TileObject tileObject;
        public bool pathReached;

        private bool isPaused;

        private string[] typeFilter;

        public delegate void PathReached();
        public event PathReached onPathReached;


        private void Awake()
        {
            pathfinding = GetComponent<Pathfinding>();
            tileObject = GetComponent<TileObject>();
        }

        public void GetPathTo(Tile tile, string[] typeFilter)
        {
            this.typeFilter = typeFilter;
            pathToTarget = pathfinding.FindPath(EasyGrid.GetTileAt(transform.position).index, tile.index, typeFilter);
            StopAllCoroutines();
            StartCoroutine(MoveToPathOverTime());
        }

        public void PausePathfinding()
        {
            isPaused = true;
        }

        public void ResumePathfinding()
        {
            isPaused = false;
        }

        IEnumerator MoveToPathOverTime()
        {
            //Tile currentTile = EasyGrid.GetTileAt(transform.position);
            int index = 0;
            pathReached = false;

            while(index < pathToTarget.Count)
            {
                //currentTile = EasyGrid.GetTileAt(transform.position);

                if (isPaused || GameManager.Instance.GamePaused)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                yield return new WaitForSeconds(2);

                if (GameManager.Instance.GamePaused)
                {
                    continue;
                }

                var thisTile = EasyGrid.GetTileAt(transform.position);
                if (thisTile.ContainsTileObjectByType("People"))
                    continue;


                Tile nextTile = pathToTarget[index];

                var hasPeople = nextTile.ContainsTileObjectByType("People");

                var tileHasFilterTileObj = pathfinding.ContainsFilteredTileObject(nextTile, typeFilter);

                if (!tileHasFilterTileObj && nextTile.GetTileObjectCount() < 2)
                {
                    EasyGrid.GetTileAt(transform.position).RemoveTileObject(tileObject);

                    EasyGrid.AssignTileObjectToTile(gameObject, (int)nextTile.index.x, (int)nextTile.index.y);

                    index++;
                }
                else if(nextTile.GetTileObjectCount() == 2)
                {
                    GameManager.Instance.CancelCombineAt(nextTile);

                    // duplicate code, I can do better
                    EasyGrid.GetTileAt(transform.position).RemoveTileObject(tileObject);

                    EasyGrid.AssignTileObjectToTile(gameObject, (int)nextTile.index.x, (int)nextTile.index.y);

                    index++;

                }
                else
                {
                    var target = pathToTarget[pathToTarget.Count - 1];
                    pathToTarget = pathfinding.FindPath(EasyGrid.GetTileAt(transform.position).index, target.index, typeFilter);
                    index = 0;
                }


            }

            pathReached = true;
            onPathReached?.Invoke();

            //print("Finished");

        }
    }
}
