using UnityEngine;
using System.Collections.Generic;
using OfFogAndDust.Company;

namespace OfFogAndDust.Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;
        public MapView view;
        public Dictionary<string, Map> storedMaps;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Map map = GenerateMap(new MapGenerationSettings
            {
                maxNodeNumber = 30,
                maxNodePerRoot = 3
            });
            view.ScaleTree(map);
            DisplayMap(map);

            // temporary, for TESTS
            CompanyManager.Instance.location = map.entrance.root.relatedGameObject;
            view.DisplayReachableLocations(CompanyManager.Instance.location, map);
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

