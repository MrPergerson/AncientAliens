using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class TractorBeam : MonoBehaviour
    {
        public Tile GetTileBelow()
        {

            Tile tile = Grid.GetTileAt(transform.position);

            //print(tile.index);


            return tile;
        }
    }


}
