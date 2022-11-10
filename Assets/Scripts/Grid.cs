using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{

    public static class Grid
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
                    Tile Tile = new Tile(new Vector2(i,j), pos, TileSize);
                    Tiles[i, j] = Tile;
                }
            }
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

            if (x < 0 || x >= SizeX || z < 0 || z >= SizeY)
                return null;

            int i = (int)Mathf.Floor(position.x / TileSize);
            int j = (int)Mathf.Floor(position.z / TileSize);

            return Tiles[i,j];
        }



    }
}
