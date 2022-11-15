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

            while(index < pathToTarget.Count)
            {
                //currentTile = EasyGrid.GetTileAt(transform.position);

                yield return new WaitForSeconds(1);

                Tile nextTile = pathToTarget[index];

                if (nextTile.IsEmpty())
                {
                    EasyGrid.GetTileAt(transform.position).RemoveTileObject(tileObject);

                    EasyGrid.AssignTileObjectToTile(gameObject, (int)nextTile.index.x, (int)nextTile.index.y);

                    index++;
                }
                else
                {
                    break;
                }


            }

            print("Finished");

        }
    }
}
