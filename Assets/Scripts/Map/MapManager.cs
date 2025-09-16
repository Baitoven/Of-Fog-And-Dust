using UnityEngine;
using System.Collections.Generic;
using OfFogAndDust.Company;
using UnityEngine.UI;
using OfFogAndDust.Game;
using OfFogAndDust.Map.Types;

namespace OfFogAndDust.Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;
        public MapView view;
        public Dictionary<string, TTreeMap> storedMaps;
        public TTreeMap currentMap;

        [SerializeField] private Button _proceedToNextMapButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            currentMap = GenerateMap(new MapGenerationSettings
            {
                maxNodeNumber = 30,
                maxNodePerRoot = 3
            });
            view.ScaleTree(currentMap);
            DisplayMap(currentMap);

            // temporary, for TESTS
            CompanyManager.Instance.location = currentMap.entrance.root.point;
            Refresh();

            _proceedToNextMapButton.onClick.RemoveAllListeners();
            _proceedToNextMapButton.onClick.AddListener(ProceedToNextMap);
        }

        public void Refresh()
        {
            view.DisplayReachableLocations(CompanyManager.Instance.location, currentMap);

            // check if the new location is an exit
            bool exitReached = false;
            foreach (TTree exit in currentMap.exits)
            {
                if (exit.root.point == CompanyManager.Instance.location)
                {
                    exitReached = true;
                }
            }
            _proceedToNextMapButton.gameObject.SetActive(exitReached);
        }

        #region GENERATION
        private TTreeMap GenerateMap(MapGenerationSettings settings)
        {
            TTreeMap map = new TTreeMap();
            map.mapTree = map.Construct(settings);
            map.FindEntrance();
            map.FindExits(3);
            return map;
        }


        public class MapGenerationSettings
        {
            public int maxNodeNumber;
            public int maxNodePerRoot;

            public Vector2 xConstraint;
            public Vector2 yConstraint;
        }
        #endregion

        private void DisplayMap(TTreeMap map)
        {
            view.GenerateMap(map.mapTree);
            view.ColorizeAll(map);
        }

        #region SAVE
        public TTreeMap SaveMap()
        {
            return currentMap;
        }

        public void LoadMap(TTreeMap map) // FIX ME
        {
            currentMap = map;
            view.ScaleTree(currentMap);
            DisplayMap(currentMap);
            Refresh();
        }
        #endregion

        private void ProceedToNextMap()
        {
            view.ClearMap();

            currentMap = GenerateMap(new MapGenerationSettings
            {
                maxNodeNumber = 30,
                maxNodePerRoot = 3
            });
            view.ScaleTree(currentMap);
            DisplayMap(currentMap);

            // temporary, for TESTS
            CompanyManager.Instance.location = currentMap.entrance.root.point;
            Refresh();
        }
    }

}

