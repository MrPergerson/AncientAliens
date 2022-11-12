using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class Tile
    {
        TileObject[] tileObjects;

        List<Tile> AdjcentTiles;

        public Vector2 index;
        public Vector3 position;
        public Vector3 center;
        public bool isLocked;

        private float size;


        public Tile(Vector2 index, Vector3 position, float size)
        {
            this.index = index;
            this.position = position;
            this.size = size;
            FindAdjcentTiles();

            tileObjects = new TileObject[2];

            center = new Vector3(position.x + size / 2, 0, position.z + size / 2);
        }

        private void FindAdjcentTiles()
        {
            AdjcentTiles = new List<Tile>();

            int Xindex = (int)index.x;
            int Yindex = (int)index.y;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        //Debug.Log((Xindex + x) + ", " + (Yindex + y));
                        var LocationX = Xindex + x;
                        var LocationY = Yindex + y;

                        if(Grid.IndexIsInBounds(LocationX, LocationY))
                        {
                            AdjcentTiles.Add(Grid.GetTileAt(new Vector2(LocationX, LocationY)));
                        }

                    }
                }
            }
        }

        public List<Tile> GetAdjcentTiles()
        {
            return AdjcentTiles;
        }

        public bool ContainsMoveableTileObject()
        {
            if (tileObjects[0] == null) return false;

            if (isLocked) return false;

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
            GameManager.Instance.Combine(tileObjects[0], tileObjects[1], this);

            return true;
        }

        public TileObject ExtractTileObject()
        {
            if(ContainsMoveableTileObject())
            {
                var tileObj = tileObjects[0];
                tileObjects[0] = null;

                if(tileObjects[1] != null)
                {
                    tileObjects[0] = tileObjects[1];
                    tileObjects[1] = null;
                }    

                return tileObj;
            }
            else
            {
                return null;
            }
        }

        public void ClearTile()
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
