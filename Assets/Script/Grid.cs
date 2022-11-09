using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Cell[,] grid;

    [SerializeField] float cellSize = 1f;
    [SerializeField] int gridSizeX = 10;
    [SerializeField] int gridSizeY = 10;

    //[SerializeField] bool centered = true;

    public static Grid Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        grid = new Cell[gridSizeX, gridSizeY];

        for(int i = 0; i < gridSizeX; i++)
        {
            for(int j = 0; j < gridSizeY; j++)
            {
                Vector3 pos = new Vector3(i * cellSize, 0, j * cellSize);
                Cell cell = new Cell(pos, cellSize);
                grid[i, j] = cell;
            }
        }
    }

    public Vector3 SnapToGrid(Vector3 position)
    {
        print("Start:" + position.x + ", " + position.z);

        int x = (int)Mathf.Floor(position.x/cellSize);
        int z = (int)Mathf.Floor(position.z/cellSize);
        //Vector3 newPosition = new Vector3(x, 0, z);

        print("Start:" + x + ", " + z);

        if (x < 0) x = 0;
        if (x >= gridSizeX) x = gridSizeX - 1;
        if (z < 0) z = 0;
        if (z >= gridSizeY) z = gridSizeY - 1;

        return grid[x,z].center;
    }

    private void OnDrawGizmos()
    {
        if(grid != null)
        {
            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    var pos = grid[i, j].center;
                    Gizmos.DrawWireSphere(pos, .1f);
                }
            }
        }
    }


}
