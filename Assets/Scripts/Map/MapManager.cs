using UnityEngine;
using OfFogAndDust.Company;
using UnityEngine.UI;
using OfFogAndDust.Map.Types;
using OfFogAndDust.Map.Settings;

namespace OfFogAndDust.Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;
        public MapView view;
        public TLinearMap currentMap;

        [SerializeField] private Button _proceedToNextMapButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // new map generation
            TTreeMap treeMap = new TTreeMap(new MapGenerationSettings
            {
                maxNodeNumber = 30,
                maxNodePerRoot = 3,
                exitNumber = 3
            });
            currentMap = new TLinearMap(treeMap);
            view.InitialScaleMap(currentMap);
            view.DisplayMap(currentMap);

            // temporary, for TESTS
            CompanyManager.Instance.mapLocation = currentMap.entrance;
            Refresh();

            _proceedToNextMapButton.onClick.RemoveAllListeners();
            _proceedToNextMapButton.onClick.AddListener(ProceedToNextMap);
        }

        public void Refresh()
        {
            view.DisplayReachableLocations(CompanyManager.Instance.mapLocation, currentMap);

            // check if the new location is an exit
            bool exitReached = false;
            foreach (int exit in currentMap.exits)
            {
                if (exit == CompanyManager.Instance.mapLocation)
                {
                    exitReached = true;
                }
            }
            _proceedToNextMapButton.gameObject.SetActive(exitReached);
        }

        private void ProceedToNextMap()
        {
            // new map generation
            TTreeMap treeMap = new TTreeMap(new MapGenerationSettings
            {
                maxNodeNumber = 30,
                maxNodePerRoot = 3,
                exitNumber = 3
            });
            currentMap = new TLinearMap(treeMap);
            view.InitialScaleMap(currentMap);
            view.DisplayMap(currentMap);

            // temporary, for TESTS
            CompanyManager.Instance.mapLocation = currentMap.entrance;
            Refresh();
        }

        #region SAVE
        public TLinearMap SaveMap()
        {
            return currentMap;
        }

        public void LoadMap(TLinearMap map)
        {
            currentMap = map;
            view.InitialScaleMap(currentMap);
            view.DisplayMap(currentMap);
            Refresh();
        }
        #endregion

        #region ZOOM
        public void Zoom(bool zoomIn, Vector3 center)
        {
            view.Zoom(currentMap, zoomIn, center);
            view.DisplayMap(currentMap);
            view.DisplayReachableLocations(CompanyManager.Instance.mapLocation, currentMap);
        }
        #endregion
    }

}

