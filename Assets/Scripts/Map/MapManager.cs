using UnityEngine;
using OfFogAndDust.Company;
using UnityEngine.UI;
using OfFogAndDust.Map.Types;
using OfFogAndDust.Map.Settings;
using static OfFogAndDust.Map.Events.MapEvent;
using OfFogAndDust.Dialogue;

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
            // initial map generation for testing
            //TTreeMap treeMap = new TTreeMap(new MapGenerationSettings
            //{
            //    maxNodeNumber = 30,
            //    maxNodePerRoot = 3,
            //    exitNumber = 3
            //});
            //currentMap = new TLinearMap(treeMap);
            //view.InitialScaleMap(currentMap);
            //view.DisplayMap(currentMap);

            // temporary, for TESTS
            //CompanyManager.Instance.mapLocation = currentMap.FindEntrance();
            //Refresh();

            _proceedToNextMapButton.onClick.RemoveAllListeners();
            _proceedToNextMapButton.onClick.AddListener(ProceedToNextMap);
        }

        public void Refresh()
        {
            view.DisplayReachableLocations(CompanyManager.Instance.mapLocation, currentMap);
            OnLocationReached(CompanyManager.Instance.mapLocation);
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
            CompanyManager.Instance.mapLocation = currentMap.FindEntrance();
            Refresh();
        }

        #region Save
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

        #region Zoom
        public void Zoom(bool zoomIn, Vector3 center)
        {
            view.Zoom(currentMap, zoomIn, center);
            view.DisplayMap(currentMap);
            view.DisplayReachableLocations(CompanyManager.Instance.mapLocation, currentMap);
        }
        #endregion

        #region Location events
        public void OnLocationReached(int locationId)
        {
            switch (currentMap.events[locationId].type)
            {
                case EventTypeEnum.ENTRANCE:
                    //throw new System.NotImplementedException();
                    break;
                case EventTypeEnum.EXIT:
                    _proceedToNextMapButton.gameObject.SetActive(true);
                    break;
                case EventTypeEnum.DIALOG:
                    DialogueManager.Instance.OnDialogue();
                    break;
                default:
                    throw new System.NotImplementedException();
            };
        }
        #endregion
    }

}

