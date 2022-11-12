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

        public Tile GetClosestEmptyTile()
        {
            List<Tile> AdjcentTiles = FindAdjcentTiles();

            foreach(var tile in AdjcentTiles)
            {
                if (tile.GetTileObjectCount() == 0)
                    return tile;
            }

            return null;
        }

        private List<Tile> FindAdjcentTiles()
        {
            List<Tile> AdjcentTiles = new List<Tile>();

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

            return AdjcentTiles;
        }

        private TileObject PeekAtTopTileObject()
        {
            // 0 bottom
            // 1 top
            //Debug.Log("Peeked at " + this.ToString() + ". Found " + PrintTileObjects());

            if (tileObjects[1] != null) return tileObjects[1];
            if (tileObjects[0] != null) return tileObjects[0];


            return null;
        }

        private TileObject PullTileObject()
        {
            TileObject tileObject = null;

            if (tileObjects[1] != null)
            {
                tileObject = tileObjects[1];
                tileObjects[1] = null;
            }
            else if (tileObjects[0] != null)
            {
                tileObject = tileObjects[0];
                tileObjects[0] = null;
            }

            return tileObject;
        }

        private bool PushNewTileObject(TileObject obj)
        {
            if (tileObjects[0] == null)
            {
               tileObjects[0] = obj; return true;
            }

            if(tileObjects[1] == null)
            {
                tileObjects[1] = obj; return true;
            }


            return false;
        }

        public int GetTileObjectCount()
        {
            int i = 0;
            if (tileObjects[0] != null) i++;
            if (tileObjects[1] != null) i++;

            return i;
        }

        public bool AddTileObject(TileObject obj)
        {

            if (GetTileObjectCount() >= 2) return false;

            if(GetTileObjectCount() == 1)
            {
                var canShare = PeekAtTopTileObject().CanShareTile;
                if (!canShare) return false;
            }

            PushNewTileObject(obj);
            obj.transform.position = center; // tractor beam also sets position

            if(GetTileObjectCount() == 2)
            {
                GameManager.Instance.Combine(tileObjects[0], tileObjects[1], this);

            }

            return true;
        }

        public TileObject ExtractTileObject()
        {
            
            var tileObject = PeekAtTopTileObject();
            
            if (tileObject != null && tileObject.CanBeMoved)
            {
                
                return PullTileObject();
            }

            return null;

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

        public override string ToString()
        {
            return "Tile(" + index.x + ", " + index.y + ")";
        }

        public string PrintTileObjects()
        {
            string list = "[";

            if (tileObjects[0] != null) list += tileObjects[0].Type;
            else list += "null";

            list += ", ";

            if (tileObjects[1] != null) list += tileObjects[1].Type;
            else list += "null";

            return list + "]";
        }
    }
}
