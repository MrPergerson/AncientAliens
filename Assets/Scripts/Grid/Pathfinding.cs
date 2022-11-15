using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens.GridSystem
{
    public class Pathfinding : MonoBehaviour
    {
        public bool printPath;


        public List<Tile> FindPath(Vector2 startIndex, Vector2 targetIndex)
        {
            Tile startTile = EasyGrid.GetTileAt(startIndex);
            Tile targetTile = EasyGrid.GetTileAt(targetIndex);

            List<Tile> openSet = new List<Tile>();
            HashSet<Tile> closedSet = new HashSet<Tile>();
            openSet.Add(startTile); // Add start node to open

            while(openSet.Count > 0)
            {
                
                Tile tile = openSet[0]; 

                // Get tile with lowest fCost -> remove it from open, then add it to closed.
                for(int i = 1; i < openSet.Count; i++)
                {
                    if(openSet[i].fCost < tile.fCost || openSet[i].fCost == tile.fCost)
                    {
                        if (openSet[i].hCost < tile.hCost)
                            tile = openSet[i];
                    }
                }

                openSet.Remove(tile);
                closedSet.Add(tile);

                // if found target tile, then path has been found
                if(tile == targetTile)
                {               
                    return RetracePath(startTile, targetTile);
                }

                // Get costs for adjcent tiles
                foreach (Tile adjcentTile in EasyGrid.FindAdjcentTiles(tile))
                {
                    if (adjcentTile.isLocked || closedSet.Contains(adjcentTile))
                    {
                        continue;
                    }

                    int newCostToAdjcentTile = tile.gCost + GetDistance(tile, adjcentTile);
                    if(newCostToAdjcentTile < adjcentTile.gCost || !openSet.Contains(adjcentTile))
                    {
                        adjcentTile.gCost = newCostToAdjcentTile;
                        adjcentTile.hCost = GetDistance(adjcentTile, targetTile);
                        adjcentTile.parent = tile;

                        if (!openSet.Contains(adjcentTile))
                            openSet.Add(adjcentTile);
                    }
                }
            }

            var noPath = new List<Tile>();
            noPath.Add(startTile);

            return noPath;
        }

        List<Tile> RetracePath(Tile startTile, Tile endTile)
        {
            List<Tile> path = new List<Tile>();
            Tile currentTile = endTile;

            while (currentTile != startTile)
            {
                path.Add(currentTile);
                currentTile = currentTile.parent;
            }

            path.Reverse();

            if(printPath)
            {
                string pathToString = gameObject.name + ": [";
                foreach (Tile tile in path)
                {
                    pathToString += " (" + tile.index.x + ", " + tile.index.y + "),";
                }
                pathToString += "]\n";
                //sprint(pathToString);
            }

            return path;
        }

        int GetDistance(Tile a, Tile b)
        {
            int distX = Mathf.Abs((int)a.index.x - (int)b.index.x);
            int distY = Mathf.Abs((int)a.index.y - (int)b.index.y);

            if (distX > distY)
                return 14 * distY + 10 * (distX * distY);
            return 14 * distX + 10 * (distY - distX);
        }
    }
}
