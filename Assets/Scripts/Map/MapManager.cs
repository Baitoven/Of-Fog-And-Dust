using UnityEngine;
using System.Collections.Generic;
using OfFogAndDust.Company;
using UnityEngine.UI;
using OfFogAndDust.Game;

namespace OfFogAndDust.Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;
        public MapView view;
        public Dictionary<string, Map> storedMaps;
        public Map currentMap;

        // TO REMOVE
        public Button temp_LaunchCombatButton;

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

            // disable me
            temp_LaunchCombatButton.onClick.AddListener(GameManager.Instance.LaunchCombat);
        }

        public void Refresh()
        {
            view.DisplayReachableLocations(CompanyManager.Instance.location, currentMap);
        }

        private Map GenerateMap(MapGenerationSettings settings)
        {
            Map map = new Map();
            map.mapTree = map.Construct(settings);
            map.FindEntrance();
            map.FindExits(3);
            return map;
        }

        private void DisplayMap(Map map)
        {
            view.GenerateMap(map.mapTree);
            view.ColorizeAll(map);
        } 

        public class MapGenerationSettings
        {
            public int maxNodeNumber;
            public int maxNodePerRoot;

            public Vector2 xConstraint;
            public Vector2 yConstraint;
        }

    }

}

