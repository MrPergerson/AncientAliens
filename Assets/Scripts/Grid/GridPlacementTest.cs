using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens.GridSystem
{
    public class GridPlacementTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                transform.position = EasyGrid.SnapToGrid(transform.position);
            }
        }
    }
}
