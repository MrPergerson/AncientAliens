using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.Combinations;
using AncientAliens.GridSystem;


namespace AncientAliens
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] int _wonderBuildProgress = 20;

        [SerializeField] float tileSize = 1;
        [SerializeField] int gridSizeX = 10;
        [SerializeField] int gridSizeZ = 10;

        [Header("TileObjects")]
        [SerializeField] GameObject People;
        [SerializeField] GameObject SandStone;
        [SerializeField] GameObject SandBrick;
        [SerializeField] GameObject Barbarian;
        [SerializeField] GameObject Wonder;

        [Header("Combinations")]
        [SerializeField] GameObject PeopleAndPeopleCombine;
        [SerializeField] GameObject PeopleAndSandStoneCombine;
        [SerializeField] GameObject SandBrickAndWonderCombine;

        List<Tile> _adjacentWonderTiles;

        public int WonderBuildProgress
        {
            get { return _wonderBuildProgress; }
            set { _wonderBuildProgress = value; }
        }

        public List<Tile> AdjacentWonderTiles
        {
            get { return _adjacentWonderTiles; }
            private set { _adjacentWonderTiles = value; }
        }

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

             EasyGrid.InitializeGrid(tileSize, gridSizeX, gridSizeZ);

            SetUpLevel();
            //var result2 = Grid.AssignTileObjectToTile(Instantiate(People), 3, 5);
            //print(result2);

        }

        public void Combine(TileObject a, TileObject b, Tile location)
        {
            List<string> types = new List<string>();
            types.Add(a.Type);
            types.Add(b.Type);

            var success = false;

            if (types.Contains("People") && types.Contains("SandStone"))
            {

                var combineObj = Instantiate(PeopleAndSandStoneCombine);
                if (combineObj.TryGetComponent(out PeopleAndSandStoneCombine combine))
                {
                    combine.Execute(a, b, location);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (types[0] == "People" && types[1] == "People")
            {

                var combineObj = Instantiate(PeopleAndPeopleCombine);
                if (combineObj.TryGetComponent(out PeopleAndPeopleCombine combine))
                {
                    combine.Execute(a, b, location);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (types.Contains("SandBrick") && types.Contains("Wonder"))
            {

                var combineObj = Instantiate(SandBrickAndWonderCombine);
                if (combineObj.TryGetComponent(out SandBrickAndWonderCombine combine))
                {
                    combine.Execute(a, b, location);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (success == false) print("Unable to combine " + types.ToString());

        }

        private void PlaceWonderInLevel()
        {
            GameObject[] wonderObjs = new GameObject[4];
            Vector2[] tiles = new Vector2[4];

            for (int i = 0; i < wonderObjs.Length; i++)
            {
                wonderObjs[i] = Instantiate(Wonder);
            }

            tiles[0] = new Vector2(2, 4);
            tiles[1] = new Vector2(3, 4);
            tiles[2] = new Vector2(2, 3);
            tiles[3] = new Vector2(3, 3);

            EasyGrid.AssignWonderToGrid(wonderObjs, tiles);
        }

        private void SetAdjacentWonderTiles()
        {
            var wonderTileObjects = EasyGrid.FindTileObjectsByType("Wonder");
            AdjacentWonderTiles = new List<Tile>();

            foreach(var tileObjects in wonderTileObjects)
            {
                var tile = EasyGrid.GetTileAt(tileObjects.transform.position);

                var adjacentTiles = EasyGrid.FindAdjcentTiles(tile);

                foreach(var adjacentTile in adjacentTiles)
                {
                    if(adjacentTile.IsEmpty() && !AdjacentWonderTiles.Contains(adjacentTile))
                    {
                        AdjacentWonderTiles.Add(adjacentTile);
                    }

                }
            }

            
        }


        private void SetUpLevel()
        {
            PlaceWonderInLevel();
            SetAdjacentWonderTiles();


            var result1 = EasyGrid.AssignTileObjectToTile(Instantiate(People), 8, 3);
            var result2 = EasyGrid.AssignTileObjectToTile(Instantiate(People), 3, 5);
            var result3 = EasyGrid.AssignTileObjectToTile(Instantiate(People), 6, 5);

            var result4 = EasyGrid.AssignTileObjectToTile(Instantiate(SandStone), 8, 8);
            var result5 = EasyGrid.AssignTileObjectToTile(Instantiate(SandStone), 7, 4);
        }
    }



}
