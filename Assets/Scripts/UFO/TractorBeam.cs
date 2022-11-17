using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;


namespace AncientAliens
{
    public class TractorBeam : MonoBehaviour
    {
        TileObject tileObject;
        [SerializeField] bool DEBUG;

        public Tile GetTileBelow()
        {

            Tile tile = EasyGrid.GetTileAt(transform.position);

            //print(tile.index);


            return tile;
        }

        public string GetTileObjectType()
        {
            if (HasTileObject())
                return tileObject.Type;
            else
                return "";
        }

        public bool HasTileObject()
        {
            return tileObject != null;
        }

        public bool PickUpTileObject()
        {
            var tile = GetTileBelow();

            if (tile == null)
            {
                if (DEBUG) print(gameObject + ": Tile is NULL");
                return false;
            }

            var tileObject = tile.ExtractTopTileObject();

            if (tileObject == null)
            {
                if (DEBUG) print(gameObject + ": Failed to get tileObject");
                return false;
            }

            this.tileObject = tileObject;
            SetTileObjectPositionToTractorBeam();

            return true;
        }

        public bool DropTileObject()
        {
            var tile = GetTileBelow();

            if (tile == null)
            {
                if(DEBUG) print(gameObject + ": Tile is NULL");
                return false;
            }

            // IF pyramid ... this doesn't belong here but it's an easy solution

            var result = tile.AddTileObject(tileObject);

            if(result == true)
            {
                SetTileObjectPositionToTile(tile);
                tileObject = null;
                return true;
            }
            else
            {
                if (DEBUG) print(gameObject + ": Failed to add tileObject to tile at " + tile.index);
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
