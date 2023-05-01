using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Map
{
    public class MapView : MonoBehaviour
    {
        [Header("Prefabs & Holder")]
        [SerializeField] private GameObject _locationPointPrefab;
        [SerializeField] private GameObject _pathPrefab;
        [SerializeField] private RectTransform _pathHolderRectTransform;
        [SerializeField] private RectTransform _locationHolderRectTransform;

        private List<MapPath> _pathList;

        public void ScaleTree(Map map)
        {
            // STEP 1 : Find xMedium and yMedium and align them on the origin
            // find x minimum
            Vector3 xMinNode = map.FindOnFunction((v1, v2) => v1.x < v2.x);
            // find x maximum
            Vector3 xMaxNode = map.FindOnFunction((v1, v2) => v1.x > v2.x);
            // find y minimum
            Vector3 yMinNode = map.FindOnFunction((v1, v2) => v1.y < v2.y);
            // find y maximum
            Vector3 yMaxNode = map.FindOnFunction((v1, v2) => v1.y > v2.y);

            float xMedium = (xMinNode.x + xMaxNode.x) / 2;
            float yMedium = (yMinNode.y + yMaxNode.y) / 2;

            map.locations = new List<Vector3>();
            map.ApplyTreeFunction((v) => v - new Vector3(xMedium, yMedium, 0f), map.mapTree);

            // STEP 2 : rescale points based on xMax/trueXMax and yMax/trueYMax
            // find x maximum
            xMaxNode = map.FindOnFunction((v1, v2) => v1.x > v2.x);
            // find y maximum
            yMaxNode = map.FindOnFunction((v1, v2) => v1.y > v2.y);

            float trueXMax = _locationHolderRectTransform.rect.xMax - 50;
            float trueYMax = _locationHolderRectTransform.rect.yMax - 50;

            float xScale = trueXMax / xMaxNode.x;
            float yScale = trueYMax / yMaxNode.y;

            map.locations = new List<Vector3>();
            map.ApplyTreeFunction((v) => new Vector3(v.x * xScale - (_locationHolderRectTransform.rect.xMax - 50), v.y * yScale, 0f) , map.mapTree);
        }

        public void GenerateMap(Map.Tree tree)
        {
            LocationPoint rootPoint = InstantiateNewPointLocation(tree.root.location);
            tree.root.point = rootPoint;

            foreach (Map.Tree t in tree.children)
            {
                GenerateMap(t);
            }
        }

        private LocationPoint InstantiateNewPointLocation(Vector2 newPointLocation)
        {
            return Instantiate(_locationPointPrefab,
                new Vector3(_locationHolderRectTransform.rect.xMax - 50, 0) + _locationHolderRectTransform.position + new Vector3(newPointLocation.x, newPointLocation.y, 0f),
                Quaternion.identity, _locationHolderRectTransform).GetComponent<LocationPoint>();
        }

        #region Colorization
        public void Colorize(LocationPoint point, Color color) 
        {
            point.image.color = color;
        }

        public void ColorizeAll(Map map)
        {
            Colorize(map.entrance.root.point, Color.blue);
            foreach (Map.Tree exit in map.exits)
            {
                Colorize(exit.root.point, Color.red);
            }
        }

        #endregion

        #region Company movement
        public void DisplayReachableLocations(LocationPoint currentLocation, Map map)
        {
            ClearPaths();
            Queue<Map.Tree> queue = new Queue<Map.Tree>();
            List<Map.Tree> result = new List<Map.Tree>();
            queue.Enqueue(map.mapTree);

            while (queue.Count > 0)
            {
                Map.Tree currentTree = queue.Dequeue();
                LocationPoint currentPoint = currentTree.root.point;
                if ((currentPoint.gameObject.transform.position - currentLocation.transform.position).magnitude < 300f && currentPoint.gameObject != currentLocation)
                {
                    result.Add(currentTree);
                    GeneratePath(currentLocation.gameObject, currentPoint.gameObject);
                    currentPoint.SetEnable();
                }
                else
                {
                    currentPoint.SetDisable();
                }

                for (int i = 0; i < currentTree.children.Count; i++)
                {
                    queue.Enqueue(currentTree.children[i]);
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
    }
}

