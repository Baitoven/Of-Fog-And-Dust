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

        public void GenerateMap(Map.Tree tree)
        {
            GameObject rootGameObject = InstantiateNewPointLocation(tree.root.location);
            tree.root.relatedGameObject = rootGameObject;

            foreach (Map.Tree t in tree.children)
            {
                GenerateMap(t);
            }
        }

        private GameObject InstantiateNewPointLocation(Vector2 newPointLocation)
        {
            return Instantiate(_locationPointPrefab,
                new Vector3(_locationHolderRectTransform.rect.xMax - 50, 0) + _locationHolderRectTransform.position + new Vector3(newPointLocation.x, newPointLocation.y, 0f),
                Quaternion.identity, _locationHolderRectTransform);
        }
    }
}

