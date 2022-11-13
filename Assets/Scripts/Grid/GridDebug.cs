using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;

namespace AncientAliens.GridSystem
{
    public class GridDebug : MonoBehaviour
    {

        [SerializeField] GameObject TestTileObject1;
        [SerializeField] GameObject TestTileObject2;


        private void Start()
        {
            if (TestTileObject1 && TestTileObject2)
            {
                var result1 = EasyGrid.AssignTileObjectToTile(TestTileObject1, 8, 3);
                var result2 = EasyGrid.AssignTileObjectToTile(TestTileObject2, 3, 5);
                //print(result);

            }

        }

        private void OnDrawGizmos()
        {
            var yoffset = new Vector3(0, .005f, 0);
            Gizmos.DrawWireCube(new Vector3(EasyGrid.SizeX / 2, 0, EasyGrid.SizeY / 2) + yoffset, new Vector3(EasyGrid.SizeX, 0, EasyGrid.SizeY));

            if (EasyGrid.Tiles != null)
            {
                for (int i = 0; i < EasyGrid.SizeX; i++)
                {
                    for (int j = 0; j < EasyGrid.SizeY; j++)
                    {

                        var pos = EasyGrid.Tiles[i, j].center;
                        Gizmos.DrawWireSphere(pos, .05f);
                        Gizmos.DrawWireCube(pos + yoffset, new Vector3(1,0,1) * (EasyGrid.TileSize - .1f));
                    }
                }
            }
        }
    }
}
