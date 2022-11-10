using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class GridDebug : MonoBehaviour
    {
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
