using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] float tileSize = 1;
        [SerializeField] int gridSizeX = 10;
        [SerializeField] int gridSizeZ = 10;

        [Header("TileObjects")]
        [SerializeField] GameObject People;
        [SerializeField] GameObject SandStone;
        [SerializeField] GameObject SandBrick;
        [SerializeField] GameObject Barbarian;
        [SerializeField] GameObject Wonder;

        [Header("DEBUG Combinations")]
        [SerializeField] GameObject TestCombine;

        [Header("DEBUG TileObjects")]
        [SerializeField] GameObject DEBUGTest1;
        [SerializeField] GameObject DEBUGTest2;
        [SerializeField] GameObject DEBUGTest3;

        private void Awake()
        {

            if(Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            Grid.InitializeGrid(tileSize, gridSizeX, gridSizeZ);

            GameObject[] wonderObjs = new GameObject[4];
            Vector2[] tiles = new Vector2[4];

            for(int i = 0; i < wonderObjs.Length; i++)
            {
                wonderObjs[i] = Instantiate(Wonder);
            }

            tiles[0] = new Vector2(2, 4);
            tiles[1] = new Vector2(3, 4);
            tiles[2] = new Vector2(2, 3);
            tiles[3] = new Vector2(3, 3);

            Grid.AssignWonderToGrid(wonderObjs, tiles);

        }

        public void Combine(TileObject a, TileObject b, Tile location)
        {
            print("combine requested");
            if((a.Type == "TestBlue" && b.Type == "TestRed") || (a.Type == "TestRed" && b.Type == "TestBlue"))
            {

                var combineObj = Instantiate(TestCombine);
                if(combineObj.TryGetComponent(out TestCombine testCombine))
                {
                    testCombine.Execute(a, b, location);
                } else { Debug.LogError("Missing class"); }
            }
        }
    }



}
