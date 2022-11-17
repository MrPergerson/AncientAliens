using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;

namespace AncientAliens
{
    [RequireComponent(typeof(Pathfinding), typeof(TileObject))]
    public class PathMovement : MonoBehaviour
    {
        private Pathfinding pathfinding;
        List<Tile> pathToTarget;
        [SerializeField] TileObject tileObject;
        public bool pathReached;

        public delegate void PathReached();
        public event PathReached onPathReached;


        private void Awake()
        {
            pathfinding = GetComponent<Pathfinding>();
            tileObject = GetComponent<TileObject>();
        }

        public void GetPathTo(Tile tile)
        {
            pathToTarget = pathfinding.FindPath(EasyGrid.GetTileAt(transform.position).index, tile.index);
            StopAllCoroutines();
            StartCoroutine(MoveToPathOverTime());
        }

        IEnumerator MoveToPathOverTime()
        {
            //Tile currentTile = EasyGrid.GetTileAt(transform.position);
            int index = 0;
            pathReached = false;

            while(index < pathToTarget.Count)
            {
                //currentTile = EasyGrid.GetTileAt(transform.position);

                if (GameManager.Instance.GamePaused)
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

                if (nextTile.IsEmpty() || (!nextTile.isLocked && hasPeople))
                {
                    EasyGrid.GetTileAt(transform.position).RemoveTileObject(tileObject);

                    EasyGrid.AssignTileObjectToTile(gameObject, (int)nextTile.index.x, (int)nextTile.index.y);

                    index++;
                }


            }

            pathReached = true;
            onPathReached?.Invoke();

            //print("Finished");

        }
    }
}
