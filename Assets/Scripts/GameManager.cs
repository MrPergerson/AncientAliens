using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.Combinations;
using AncientAliens.GridSystem;
using UnityEngine.InputSystem;
using AncientAliens.UI;
using AncientAliens.TileObjects;

namespace AncientAliens
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private bool _gamePaused;
        [SerializeField] private bool _gameOver;

        [SerializeField] int _wonderBuildProgress = 20;

        [SerializeField] float tileSize = 1;
        [SerializeField] int gridSizeX = 10;
        [SerializeField] int gridSizeZ = 10;

        [Header("TileObjects")]
        [SerializeField] GameObject People;
        [SerializeField] GameObject SandStone;
        [SerializeField] GameObject SandBrick;
        [SerializeField] GameObject City;
        [SerializeField] GameObject Wonder;

        [Header("Combinations")]
        [SerializeField] GameObject PeopleAndPeopleCombine;
        [SerializeField] GameObject PeopleAndSandStoneCombine;
        [SerializeField] GameObject PeopleAndBarbarianCombine;
        [SerializeField] GameObject PeopleAndSandBrickCombine;
        [SerializeField] GameObject CityAndPeopleCombine;
        [SerializeField] GameObject SandBrickAndWonderCombine;
        [SerializeField] GameObject SandBrickAndBarbarianCombine;

        public List<TileObjectCombine> activeCombinations = new List<TileObjectCombine>();

        public int _peopleCount = 0;

        List<Tile> _adjacentWonderTiles;

        Controls controls;



        public int WonderBuildProgress
        {
            get { return _wonderBuildProgress; }
            set 
            { 
                _wonderBuildProgress = value;

                if (!GameOver && _wonderBuildProgress >= GameRules.maxWonderProgress)
                    GameWin();
                else if (!GameOver && _wonderBuildProgress <= 0)
                    GameLost();
            }
        }

        public int PeopleCount
        {
            get { return _peopleCount; }
            set
            {
                _peopleCount = value;

                if (!GameOver && PeopleCount <= 0)
                    GameLost();
            }
        }

        public bool GameOver
        {
            get { return _gameOver; }
            private set { _gameOver = value; }
        }

        public List<Tile> AdjacentWonderTiles
        {
            get { return _adjacentWonderTiles; }
            private set { _adjacentWonderTiles = value; }
        }

        public bool GamePaused
        {
            get { return _gamePaused; }
            set { _gamePaused = value; }
        }

        private void OnEnable()
        {
            controls.Enable();

            controls.Player.Menu.performed += SetMenuState;
        }

        private void OnDisable()
        {
            controls.Disable();

            controls.Player.Menu.performed -= SetMenuState;
        }

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

            GameOver = false;

            controls = new Controls();

            EasyGrid.InitializeGrid(tileSize, gridSizeX, gridSizeZ);


            SetUpLevel();
            //var result2 = Grid.AssignTileObjectToTile(Instantiate(People), 3, 5);
            //print(result2);

        }

        private void SetMenuState(InputAction.CallbackContext ctx)
        {
            if (GamePaused)
                CloseMenu();
            else
                OpenMenu();
        }

        public void OpenMenu()
        {
            GamePaused = true;
            UIManager.Instance.OpenPauseMenu();
            Time.timeScale = 0;
        }

        public void CloseMenu()
        {
            GamePaused = false;
            Time.timeScale = 1;
            UIManager.Instance.CloseMenus();
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
                    activeCombinations.Add(combine);
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
                    activeCombinations.Add(combine);
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
                    activeCombinations.Add(combine);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (types.Contains("People") && types.Contains("SandBrick"))
            {
                var combineObj = Instantiate(PeopleAndSandBrickCombine);
                if (combineObj.TryGetComponent(out PeopleAndSandBrickCombine combine))
                {
                    combine.Execute(a, b, location);
                    activeCombinations.Add(combine);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (types.Contains("City") && types.Contains("People"))
            {
                var combineObj = Instantiate(CityAndPeopleCombine);
                if (combineObj.TryGetComponent(out CityAndPeopleCombine combine))
                {
                    combine.Execute(a, b, location);
                    activeCombinations.Add(combine);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (types.Contains("Barbarian") && types.Contains("People"))
            {
                var combineObj = Instantiate(PeopleAndBarbarianCombine);
                if (combineObj.TryGetComponent(out PeopleAndBarbarianCombine combine))
                {
                    combine.Execute(a, b, location);
                    activeCombinations.Add(combine);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (types.Contains("Barbarian") && types.Contains("SandBrick"))
            {
                var combineObj = Instantiate(SandBrickAndBarbarianCombine);
                if (combineObj.TryGetComponent(out SandBrickAndBarbarianCombine combine))
                {
                    combine.Execute(a, b, location);
                    activeCombinations.Add(combine);
                    success = true;
                }
                else { Debug.LogError("Missing class"); }
            }

            if (types.Contains("SandStone") && types.Contains("SandBrick"))
            {
                success = true; // ignore
            }

            if (success == false) print("Unable to combine " + types.ToString());

        }

        public void CancelCombineAt(Tile location)
        {
            foreach(var combine in activeCombinations)
            {
                if(combine.Location.Equals(location))
                {
                    combine.Cancel();
                    return;
                }
            }
        }

        private void PlaceWonderInLevel()
        {
            GameObject[] wonderObjs = new GameObject[4];
            Vector2[] tiles = new Vector2[4];

            for (int i = 0; i < wonderObjs.Length; i++)
            {
                wonderObjs[i] = Instantiate(Wonder);
            }

            tiles[0] = new Vector2(7, 8);
            tiles[1] = new Vector2(8, 8);
            tiles[2] = new Vector2(7, 7);
            tiles[3] = new Vector2(8, 7);

            EasyGrid.AssignWonderToGrid(wonderObjs, tiles);
        }

        private void SetAdjacentWonderTiles()
        {
            var wonderTileObjects = EasyGrid.FindTileObjectsByType("Wonder");
            AdjacentWonderTiles = new List<Tile>();

            foreach (var tileObjects in wonderTileObjects)
            {
                var tile = EasyGrid.GetTileAt(tileObjects.transform.position);

                var adjacentTiles = EasyGrid.FindAdjcentTiles(tile);

                foreach (var adjacentTile in adjacentTiles)
                {
                    if (adjacentTile.IsEmpty() && !AdjacentWonderTiles.Contains(adjacentTile))
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


            EasyGrid.AssignTileObjectToTile(Instantiate(People), 8, 3);
            EasyGrid.AssignTileObjectToTile(Instantiate(People), 3, 5);
            EasyGrid.AssignTileObjectToTile(Instantiate(People), 6, 5);

            EasyGrid.AssignTileObjectToTile(Instantiate(SandBrick), 9, 5);

            for (int i = 0; i < 10; i++)
            {
                var point = GetRandomPointOnGrid();
                EasyGrid.AssignTileObjectToTile(Instantiate(SandStone), (int)point.x, (int)point.y);
            }

        }

        private Vector2 GetRandomPointOnGrid()
        {
            var x = Random.Range(3, gridSizeX-3);
            var y = Random.Range(3, gridSizeZ-3);
            var point = new Vector2(x, y);

            if (EasyGrid.GetTileAt(point).IsEmpty())
                return point;
            else
                return GetRandomPointOnGrid(); // careful...
        }

        private void GameWin()
        {
            GamePaused = true;
            UIManager.Instance.OpenWinScreen();
            GameOver = true;
        }

        private void GameLost()
        {
            GamePaused = true;
            UIManager.Instance.OpenLoseScreen();
            GameOver = true;
        }
    }



}
