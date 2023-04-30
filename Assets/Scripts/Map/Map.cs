using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Map
{
    public class Map : MonoBehaviour
    {
        public class Path
        {
            GameObject sourcePoint;
            GameObject path;
        }

        [Header("Parameters")]
        [SerializeField] private int _locationNumber;
        [SerializeField] private float _pathProbability;

        [Header("Prefabs & Holder")]
        [SerializeField] private GameObject _locationPointPrefab;
        [SerializeField] private GameObject _pathPrefab;
        [SerializeField] private RectTransform _pathHolderRectTransform;
        [SerializeField] private RectTransform _locationHolderRectTransform;

        private List<GameObject> _locations;
        private List<Path> _paths;

        private void Start()
        {
            _locations = new List<GameObject>();
            GenerateMap();
        }

        public void GenerateMap()
        {
            for (int i = 0; i < _locationNumber; i++)
            {
                GeneratePoint();
            }
            GameObject entrance = FindEntrance();
            entrance.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            List<GameObject> exits = FindExits();
            foreach (GameObject go in exits)
            {
                go.transform.GetChild(0).GetComponent<Image>().color = Color.blue;
            }

            List<List<GameObject>> neighbors = FindAllNeighbors(250f);
            for (int i = 0; i < neighbors.Count; i++)
            {
                foreach (GameObject n in neighbors[i])
                {
                    GeneratePathAux(_locations[i], n);
                }
            }
        }

        #region Generation

        // TODO : rework point generations based on a tree so there is always a path to exits
        private void GeneratePoint()
        {
            bool newPositionValid = false;
            Vector3 newPosition = RandomizeLocation();
            while (!newPositionValid)
            {
                if (_locations.Count == 0)
                    newPositionValid = true;

                foreach (GameObject l in _locations)
                {
                    if ((l.transform.position - _locationHolderRectTransform.position - newPosition).magnitude < 50)
                    {
                        newPositionValid = false;
                        newPosition = RandomizeLocation();
                        break;
                    }
                    newPositionValid = true;
                }
            }

            GameObject location = Instantiate(_locationPointPrefab, _locationHolderRectTransform.position + newPosition, Quaternion.identity, _locationHolderRectTransform);
            _locations.Add(location);
        }

        private void GeneratePath(GameObject currentPoint) 
        {
            List<GameObject> closestPoints = FindClosestPoints(currentPoint);
            foreach (GameObject point in closestPoints)
            {
                GeneratePathAux(currentPoint, point);
            }
        } 

        private void GeneratePathAux(GameObject currentPoint, GameObject target)
        {
            if (Random.value < _pathProbability)
            {
                float angle = Vector3.Angle(target.transform.position - currentPoint.transform.position, Vector3.up);
                Quaternion rot = new Quaternion();
                rot.SetFromToRotation(Vector3.up, target.transform.position - currentPoint.transform.position);
                GameObject newPath = Instantiate(_pathPrefab, (target.transform.position + currentPoint.transform.position) / 2, rot, _pathHolderRectTransform);
                newPath.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, (currentPoint.transform.position - target.transform.position).magnitude - 50f);
            }
        }

        private Vector3 RandomizeLocation()
        {
            return new Vector3(
                Random.Range(_locationHolderRectTransform.rect.xMin + 50, _locationHolderRectTransform.rect.xMax - 50),
                Random.Range(_locationHolderRectTransform.rect.yMin + 50, _locationHolderRectTransform.rect.yMax - 50),
                0f
            );
        }
        #endregion

        #region Point Find
        private GameObject? FindEntrance()
        {
            if (_locations.Count == 0) return null;

            GameObject result = _locations[0];
            foreach (GameObject l in _locations)
            {
                if (l.transform.position.x > result.transform.position.x)
                {
                    result = l;
                }
            }
            return result;
        }

        private List<GameObject>? FindExits()
        {
            if (_locations.Count == 0) return null;

            List<GameObject> result = new List<GameObject>();
            if (_locations.Count < 3) 
            {
                result.Add(_locations[0]);
                return result;
            }
            result.Add(_locations[0]);
            result.Add(_locations[1]);
            result.Add(_locations[2]);

            foreach (GameObject l in _locations)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (l.transform.position.x < result[i].transform.position.x)
                    {
                        result[i] = l;
                        break;
                    }
                }
                
            }
            return result;
        }

        private List<List<GameObject>> FindAllNeighbors(float distance) 
        {
            List<List<GameObject>> result = new List<List<GameObject>>();
            foreach (GameObject l in _locations)
            {
                result.Add(FindNeighbors(l, distance));
            }
            return result;
        }

        private List<GameObject>? FindNeighbors(GameObject currentPoint, float distance)
        {
            if (_locations.Count == 0) return null;

            List<GameObject> result = new List<GameObject>();
            foreach (GameObject l in _locations)
            {
                if (l.transform.position != currentPoint.transform.position && (l.transform.position - currentPoint.transform.position).magnitude < distance)
                {
                    result.Add(l);
                }
            }
            return result;
        } 

        private List<GameObject>? FindClosestPoints(GameObject currentPoint)
        {
            if (_locations.Count == 0) return null;

            List<GameObject> result = new List<GameObject>();
            if (_locations.Count < 3)
            {
                result.Add(_locations[0]);
                return result;
            }
            result.Add(_locations[0]);
            result.Add(_locations[1]);
            result.Add(_locations[2]);

            foreach (GameObject l in _locations)
            {
                if (l.transform.position != currentPoint.transform.position)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if ((l.transform.position - currentPoint.transform.position).magnitude < (result[i].transform.position - currentPoint.transform.position).magnitude)
                        {
                            result[i] = l;
                            break;
                        }
                    }
                }

                
            }
            return result;
        }
        #endregion
    }
}

