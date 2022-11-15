using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AncientAliens.GridSystem
{

    public static class EasyGrid
    {
        static Tile[,] _tiles;



        public static Tile[,] Tiles
        {
            get { return _tiles; }
            private set { _tiles = value; }
        }

        public static float TileSize { get; private set; }
        public static int SizeX { get; private set; }
        public static int SizeY { get; private set; }


        public static void InitializeGrid(float tileSize, int gridSizeX, int gridSizeY)
        {
            TileSize = tileSize;
            SizeX = gridSizeX;
            SizeY = gridSizeY;

            Tiles = new Tile[gridSizeX, gridSizeY];

            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    Vector3 pos = new Vector3(i * TileSize, 0, j * TileSize);
                    Tile Tile = new Tile(new Vector2(i,j), pos, TileSize, false);
                    Tiles[i, j] = Tile;
                }
            }

            //Tiles[0, 0].GetAdjcentTiles();
        }


        public static Vector3 SnapToGrid(Vector3 position)
        {

            int x = (int)Mathf.Floor(position.x / TileSize);
            int z = (int)Mathf.Floor(position.z / TileSize);

            if (x < 0) x = 0;
            if (x >= SizeX) x = SizeX - 1;
            if (z < 0) z = 0;
            if (z >= SizeY) z = SizeY - 1;

            return Tiles[x, z].center;
        }

        public static Tile GetTileAt(Vector3 position)
        {
            var x = position.x;
            var z = position.z;

            if (!IndexIsInBounds(x,z))
                return null;

            int i = (int)Mathf.Floor(position.x / TileSize);
            int j = (int)Mathf.Floor(position.z / TileSize);

            return Tiles[i,j];
        }

        public static Tile GetTileAt(Vector2 index)
        {

            if (!IndexIsInBounds(index.x, index.y))
                return null;

            return Tiles[(int)index.x, (int)index.y];
        }

        public static List<Tile> FindAdjcentTiles(Tile tile)
        {
            List<Tile> AdjcentTiles = new List<Tile>();

            int Xindex = (int)tile.index.x;
            int Yindex = (int)tile.index.y;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        //Debug.Log((Xindex + x) + ", " + (Yindex + y));
                        var LocationX = Xindex + x;
                        var LocationY = Yindex + y;

                        if (IndexIsInBounds(LocationX, LocationY))
                        {
                            AdjcentTiles.Add(GetTileAt(new Vector2(LocationX, LocationY)));
                        }

                    }
                }
            }

            return AdjcentTiles;
        }

        public static bool AssignTileObjectToTile(GameObject tileObject, int x, int y)
        {
            if (!IndexIsInBounds(x, y))
                return false;

            var obj = tileObject.GetComponent<TileObject>();
            if (obj == null) return false;

            var result = Tiles[x, y].AddTileObject(obj);

            return result;
        }

        public static bool AssignWonderToGrid(GameObject[] tileObjects, Vector2[] tiles)
        {
            //TODO needs validation test
            /*  
             *  ->  @@
             *      @@
             */

            if (tileObjects.Length != 4 || tileObjects.Length != 4)
                return false;


            var result = true;
            for (int i = 0; i < 4; i++)
            {
                result = AssignTileObjectToTile(tileObjects[i], (int)tiles[i].x, (int)tiles[i].y);

                if (result == false)
                    break;
            }

            if(result == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    var tile = EasyGrid.GetTileAt(tiles[i]);

                    if (tile != null) tile.ClearTile();
                }
            }


            return true;
        }

        public static List<TileObject> FindTileObjectsByType(string type)
        {
            List<TileObject> list = new List<TileObject>();

            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {

                    var tileObject = Tiles[i, j].PeekAtTileObjectOfType(type);
                    if (tileObject != null)
                        list.Add(tileObject);
                }
            }

            return list;
        }

        public static bool IndexIsInBounds(float x, float y)
        {
            return ((x >= 0 && x < SizeX) && (y >= 0 && y < SizeY));
        }


    }
}
