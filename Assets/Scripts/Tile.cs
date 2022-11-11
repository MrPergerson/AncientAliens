using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class Tile
    {
        TileObject[] tileObjects;


        public Vector2 index;
        public Vector3 position;
        public Vector3 center;

        private float size;


        public Tile(Vector2 index, Vector3 position, float size)
        {
            this.index = index;
            this.position = position;
            this.size = size;

            tileObjects = new TileObject[2];

            center = new Vector3(position.x + size / 2, 0, position.z + size / 2);
        }

        public bool ContainsMoveableTileObject()
        {
            if (tileObjects[0] == null) return false;

            if (tileObjects[0] != null && tileObjects[1] != null) return false;

            if (tileObjects[0].CanBeMoved == false) return false;

            return true;
        }

        public bool AddTileObject(TileObject obj)
        {
            if (tileObjects[0] != null && tileObjects[1] != null) return false;

            if (tileObjects[0] == null)
            {
                tileObjects[0] = obj;
                obj.transform.position = center; // tractor beam also sets position
                return true;
            }

            if (!tileObjects[0].CanShareTile) return false;

            tileObjects[1] = obj;
            obj.transform.position = center;

            return true;
        }

        public TileObject ExtractTileObject()
        {
            if(ContainsMoveableTileObject())
            {
                var tileObj = tileObjects[0];
                tileObjects[0] = null;
                return tileObj;
            }
            else
            {
                return null;
            }
        }

        public void ClearTileObjects()
        {
            tileObjects[0] = null;
            tileObjects[1] = null;
        }

        public override bool Equals(object obj)
        {
            var otherTile = obj as Tile;

            if (otherTile != null)
            {
                return this.position == otherTile.position;
            }
            else
            {
                return base.Equals(obj);

            }

        }

        public override int GetHashCode()
        {
            return this.position.GetHashCode();
        }
    }
}
