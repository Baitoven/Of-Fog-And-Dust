using OfFogAndDust.Map.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Map
{
    public class MapView : MonoBehaviour
    {
        [Header("Prefabs & Holder")]
        [SerializeField] private GameObject _locationPointPrefab;
        [SerializeField] private GameObject _pathPrefab;
        [SerializeField] private RectTransform _pathHolderRectTransform;
        [SerializeField] private RectTransform _locationHolderRectTransform;

        private List<LocationPoint> _locations = new List<LocationPoint>();
        private List<MapPath> _pathList;

        private int zoomCount = 0;

        private Vector2 mapScale = Vector2.one;

        // by default: zoom = 0 and center = Vector3.zero
        public void ScaleTree(TLinearMap map, int zoom, Vector3 center)
        {
            // STEP 1 : Find xMedium and yMedium and align them on the origin
            Vector3 xMinNode = map.FindOnFunction((v1, v2) => v1.x < v2.x);
            Vector3 xMaxNode = map.FindOnFunction((v1, v2) => v1.x > v2.x);
            Vector3 yMinNode = map.FindOnFunction((v1, v2) => v1.y < v2.y);
            Vector3 yMaxNode = map.FindOnFunction((v1, v2) => v1.y > v2.y);

            float xMedium = (xMinNode.x + xMaxNode.x) / 2;
            float yMedium = (yMinNode.y + yMaxNode.y) / 2;

            map.ApplyFunction((v) => v - new Vector3(xMedium, yMedium, 0f), map.locations);

            // STEP 2 : rescale points based on xMax/trueXMax and yMax/trueYMax
            xMaxNode = map.FindOnFunction((v1, v2) => v1.x > v2.x);
            yMaxNode = map.FindOnFunction((v1, v2) => v1.y > v2.y);

            float trueXMax = (_locationHolderRectTransform.rect.xMax - 50) * (1 + 0.1f * zoom);
            float trueYMax = (_locationHolderRectTransform.rect.yMax - 50) * (1 + 0.1f * zoom);

            mapScale = new Vector2(trueXMax / xMaxNode.x, trueYMax / yMaxNode.y);

            map.ApplyFunction((v) => new Vector3(
                v.x * mapScale.x - (_locationHolderRectTransform.rect.xMax - 50) + center.x, 
                v.y * mapScale.y + center.y, 
                0f), map.locations);
        }

        public void GenerateMap(TLinearMap map)
        {
            for (int i = 0; i < map.locations.Count; i++)
            {
                LocationPoint locPoint = InstantiateNewPointLocation(map.locations[i]);
                locPoint.pointNumber = i;
                _locations.Add(locPoint);
            }
        }

        public void DisplayMap(TLinearMap map)
        {
            // if locations is empty, they need to be instanciated
            if (_locations.Count == 0)
            {
                GenerateMap(map);
            }
            else
            {
                // reuse available points
                for (int i = 0; i < map.locations.Count; i++)
                {
                    _locations[i].gameObject.transform.position = map.locations[i];
                    _locations[i].pointNumber = i;
                }
            }
            ColorizeAll(map);
        }

        private LocationPoint InstantiateNewPointLocation(Vector2 newPointLocation)
        {
            return Instantiate(_locationPointPrefab,
                new Vector3(_locationHolderRectTransform.rect.xMax - 50, 0) + _locationHolderRectTransform.position + new Vector3(newPointLocation.x, newPointLocation.y, 0f),
                Quaternion.identity, _locationHolderRectTransform).GetComponent<LocationPoint>();
        }

        public void ClearMap()
        {
            foreach (LocationPoint location in _locations)
            {
                Destroy(location.gameObject);
            }
            _locations.Clear();
            foreach (MapPath path in _pathList)
            {
                Destroy(path.gameObject);
            }
            _pathList.Clear();
        }

        #region Colorization
        public void Colorize(LocationPoint point, Color color) 
        {
            point.image.color = color;
        }

        public void ColorizeAll(TLinearMap map)
        {
            Colorize(_locations[map.entrance], Color.blue);
            foreach (int exit in map.exits)
            {
                Colorize(_locations[exit], Color.red);
            }
        }

        #endregion

        #region Company movement
        public void DisplayReachableLocations(int currentMapLocation, TLinearMap map)
        {
            ClearPaths();
            LocationPoint currentLocation = _locations[currentMapLocation];
            foreach (LocationPoint location in _locations)
            {
                if ((location.transform.position - currentLocation.transform.position).magnitude <= 300f * Math.Max(mapScale.x, mapScale.y)
                                    && location.gameObject != currentLocation.gameObject)
                {
                    GeneratePath(currentLocation.gameObject, location.gameObject);
                    location.SetEnable();
                }
                else
                {
                    location.SetDisable();
                }
            }
        }

        private void GeneratePath(GameObject currentPoint, GameObject target)
        {
            Quaternion rot = new Quaternion();
            rot.SetFromToRotation(Vector3.up, target.transform.position - currentPoint.transform.position);
            MapPath newPath = Instantiate(_pathPrefab, (target.transform.position + currentPoint.transform.position) / 2, rot, _pathHolderRectTransform).GetComponent<MapPath>();
            newPath.rect.sizeDelta = new Vector2(5f, (currentPoint.transform.position - target.transform.position).magnitude - 50f);
            _pathList.Add(newPath);
        }

        private void ClearPaths()
        {
            if (_pathList != null)
            {
                foreach (MapPath p in _pathList)
                {
                    Destroy(p.gameObject);
                }
            }
            
            _pathList = new List<MapPath>();
        }

        #endregion

        #region Zoom
        public void Zoom(TLinearMap currentMap, bool zoomIn, Vector3 center)
        {
            zoomCount += zoomIn ? 1 : -1;
            zoomCount = Math.Min(Math.Max(zoomCount, -5), 5); // gate value to [-5;5]
            center = new Vector3(-(center.x - 0.5f) * _locationHolderRectTransform.rect.xMax, -(center.y - 0.5f) * _locationHolderRectTransform.rect.yMax, 0f);
            ScaleTree(currentMap, zoomCount, center);
        }
        #endregion
    }
}

