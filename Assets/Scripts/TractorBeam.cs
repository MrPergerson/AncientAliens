using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class TractorBeam : MonoBehaviour
    {
        TileObject tileObject;

        public Tile GetTileBelow()
        {

            Tile tile = Grid.GetTileAt(transform.position);

            //print(tile.index);


            return tile;
        }

        public bool HasTileObject()
        {
            return tileObject != null;
        }

        public bool PickUpTileObject()
        {
            var tile = GetTileBelow();

            if (tile == null) return false;

            var tileObject = tile.ExtractTileObject();

            if (tileObject == null) return false;

            this.tileObject = tileObject;
            SetTileObjectPositionToTractorBeam();

            return true;
        }

        public bool DropTileObject()
        {
            var tile = GetTileBelow();

            if (tile == null) return false;

            var result = tile.AddTileObject(tileObject);

            if(result == true)
            {
                SetTileObjectPositionToTile(tile);
                tileObject = null;
                return true;
            }
            else
            {
                return false;
            }

        }

        private void SetTileObjectPositionToTractorBeam()
        {
            tileObject.transform.parent = this.transform;
            tileObject.transform.localPosition = new Vector3(0, -0.8f, 0);
        }

        private void SetTileObjectPositionToTile(Tile tile)
        {
            tileObject.transform.parent = null;
            tileObject.transform.position = tile.center;
        }
    }


}
