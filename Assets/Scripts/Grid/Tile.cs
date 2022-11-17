using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AncientAliens.GridSystem
{
    public class Tile
    {
        TileObject[] tileObjects;

        

        public Vector2 index;
        public Vector3 worldPosition;
        public Vector3 center;
        public bool isLocked; // could mean either an obstacle or the tile is full (combining)

        public int gCost;
        public int hCost;
        public Tile parent;

        public int fCost
        {
            get { return gCost + hCost; }
        }


        public Tile(Vector2 index, Vector3 worldPosition, float size, bool locked)
        {
            this.index = index;
            this.worldPosition = worldPosition;
            isLocked = locked;

            tileObjects = new TileObject[2];

            center = new Vector3(worldPosition.x + size / 2, 0, worldPosition.z + size / 2);
        }

        public Tile GetClosestEmptyTile()
        {

            // this needs to become recursive

            List<Tile> AdjcentTiles = EasyGrid.FindAdjcentTiles(this);

            foreach(var tile in AdjcentTiles)
            {
                if (tile.GetTileObjectCount() == 0)
                    return tile;
            }

            return null;
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

        public TileObject ExtractTopTileObject()
        {
            
            var tileObject = PeekAtTopTileObject();
            
            if (tileObject != null && tileObject.CanBeMoved)
            {
                
                return PullTileObject();
            }

            return null;

        }

        public void RemoveTileObject(TileObject tileObject)
        {

            if(tileObjects[1] == tileObject)
            {
                tileObjects[1] = null;
            }
            else if(tileObjects[0] == tileObject)
            {
                tileObjects[0] = null;
                if(tileObjects[1] != null)
                {
                    tileObjects[0] = tileObjects[1];
                    tileObjects[1] = null;
                }
            }

        }

        public TileObject PeekAtTileObjectOfType(string type)
        {
            if (tileObjects[1] != null)
                return null;
            else if (tileObjects[0] != null && tileObjects[0].Type == type)
                return tileObjects[0];
            else
                return null;
        }

        public bool ContainsTileObjectByType(string type)
        {
            if (tileObjects[0] != null && tileObjects[0].Type == type) return true;
            if (tileObjects[1] != null && tileObjects[1].Type == type) return true;

            return false;
        }

        public void ClearTile()
        {
            tileObjects[0] = null;
            tileObjects[1] = null;
        }

        
        public bool IsEmpty()
        {
            return (tileObjects[0] == null && tileObjects[1] == null);
        }

        public override bool Equals(object obj)
        {
            var otherTile = obj as Tile;

            if (otherTile != null)
            {
                return this.worldPosition == otherTile.worldPosition;
            }
            else
            {
                return base.Equals(obj);

            }

        }

        public override int GetHashCode()
        {
            return this.worldPosition.GetHashCode();
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
