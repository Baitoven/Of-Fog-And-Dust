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
        [SerializeField] private float _maxNodeNumber;

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
            _ = Construct(30);

        }

        #region Tree
        public class Tree
        {
            public Tree()
            {
                root = new Node();
                children = new List<Tree>();
            }

            public class Node
            {
                public GameObject relatedGameObject;
                public Vector3 location;
            }

            public Node root;
            public List<Tree> children;
        }


        public Tree Construct(int remainingNodes)
        {
            Queue<Tree> queue = new Queue<Tree>();

            Tree result = new Tree();
            result.root.location = new Vector3(_locationHolderRectTransform.rect.xMax - 50, 0) + _locationHolderRectTransform.position;
            GameObject rootGameObject = InstanciateNewPointLocation(result.root.location);
            _locations.Add(rootGameObject);
            result.root.relatedGameObject = rootGameObject;
            remainingNodes--;
            queue.Enqueue(result);

            while (queue.Count > 0)
            {
                Tree currentTree = queue.Dequeue();
                int childrenNumber = (int)Mathf.Floor(Random.Range(1, Mathf.Min(remainingNodes, _maxNodeNumber)));

                for (int i = 0; i < childrenNumber; i++)
                {
                    Tree newTree = new Tree();
                    Vector3 newLocation = GenerateLocation(currentTree.root.location);
                    newTree.root.location = newLocation;
                    Debug.DrawLine(currentTree.root.location, newTree.root.location, Color.green, 100f);
                    // prob need to check if not too close to another
                    GameObject newRootGameObject = InstanciateNewPointLocation(newTree.root.location);
                    _locations.Add(newRootGameObject);
                    newTree.root.relatedGameObject = newRootGameObject;
                    currentTree.children.Add(newTree);
                    remainingNodes--;
                    queue.Enqueue(newTree);
                }
            }
            return result;
        }
        #endregion

        #region Generation
        private Vector3 GenerateLocation(Vector3 currentPointLocation)
        {
            bool newPositionValid = false;
            Vector3 newPosition = currentPointLocation - GetNewPointLocation(200f, 100f);
            while (!newPositionValid)
            {
                if (newPosition.x < _locationHolderRectTransform.transform.position.x + _locationHolderRectTransform.rect.xMin + 50 || newPosition.x > _locationHolderRectTransform.transform.position.x + _locationHolderRectTransform.rect.xMax - 50 ||
                    newPosition.y < _locationHolderRectTransform.transform.position.y + _locationHolderRectTransform.rect.yMin + 50 || newPosition.y > _locationHolderRectTransform.transform.position.y + _locationHolderRectTransform.rect.yMax - 50)
                {
                    newPositionValid = false;
                    newPosition = currentPointLocation - GetNewPointLocation(200f, 100f);
                }

                else
                {

                    foreach (GameObject l in _locations)
                    {
                        if ((l.transform.position - newPosition).magnitude < 50)
                        {
                            newPositionValid = false;
                            newPosition = currentPointLocation - GetNewPointLocation(200f, 100f);
                            break;
                        }
                        newPositionValid = true;
                    }
                }

            }
            return newPosition;
        }

        private Vector3 GetNewPointLocation(float maxDist, float distOffset)
        {
            float angle = Random.value * 160f + 10f; // between 10 and 170
            float radAngle = angle * 2 * Mathf.PI / 360f;
            float distance = Random.value * maxDist + distOffset;
            return new Vector3(distance * Mathf.Sin(radAngle), distance * Mathf.Cos(radAngle), 0f);
        }

        private GameObject InstanciateNewPointLocation(Vector2 newPointLocation)
        {
            return Instantiate(_locationPointPrefab, 
                new Vector3(newPointLocation.x, newPointLocation.y, 0f), 
                Quaternion.identity, _locationHolderRectTransform);
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

