using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class GridPlacementTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                transform.position = Grid.SnapToGrid(transform.position);
            }
        }
    }
}
