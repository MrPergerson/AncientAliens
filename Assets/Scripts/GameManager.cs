using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] float tileSize = 1;
        [SerializeField] int gridSizeX = 10;
        [SerializeField] int gridSizeZ = 10;

        private void Awake()
        {
            Grid.InitializeGrid(tileSize, gridSizeX, gridSizeZ);

        }
    }
}
