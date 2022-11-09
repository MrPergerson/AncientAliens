using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 position;
    public Vector3 center;

    private float size;


    public Cell(Vector3 position, float size)
    {
        this.position = position;
        this.size = size;

        center = new Vector3(position.x + size / 2, 0, position.z + size / 2);
    }
}
