using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class GridDebug : MonoBehaviour
    {

        [SerializeField] GameObject TestTileObject;

        private void Start()
        {
            if (!TestTileObject)
                Debug.LogError("TestTileObject is null");
            else
            {
                var result = Grid.AssignTileObjectToTile(TestTileObject, 2, 3);
                //print(result);

            }

        }

        private void OnDrawGizmos()
        {
            if (Grid.Tiles != null)
            {
                for (int i = 0; i < Grid.SizeX; i++)
                {
                    for (int j = 0; j < Grid.SizeY; j++)
                    {
                        var pos = Grid.Tiles[i, j].center;
                        Gizmos.DrawWireSphere(pos, .1f);
                    }
                }
            }
        }
    }
}
