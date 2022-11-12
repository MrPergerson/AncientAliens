using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class GridDebug : MonoBehaviour
    {

        [SerializeField] GameObject TestTileObject1;
        [SerializeField] GameObject TestTileObject2;


        private void Start()
        {
            if (TestTileObject1 && TestTileObject2)
            {
                var result1 = Grid.AssignTileObjectToTile(TestTileObject1, 8, 3);
                var result2 = Grid.AssignTileObjectToTile(TestTileObject2, 3, 5);
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
