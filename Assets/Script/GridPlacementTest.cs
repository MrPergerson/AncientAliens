using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = Grid.Instance.SnapToGrid(transform.position);
        }
    }
}
