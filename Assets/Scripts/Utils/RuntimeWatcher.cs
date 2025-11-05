using OfFogAndDust.Company;
using OfFogAndDust.Map;
using OfFogAndDust.Save;
using System.Collections;
using UnityEngine;

namespace OfFogAndDust.Utils
{
    public class RuntimeWatcher : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(WaitForSaveManager());
        }

        private IEnumerator WaitForSaveManager()
        {
            yield return new WaitUntil(() => SaveManager.Instance != null);
            SaveManager.Instance.LoadTutorial();
            yield return new WaitUntil(() => CompanyManager.Instance != null);
            yield return new WaitUntil(() => MapManager.Instance != null);
            CompanyManager.Instance.mapLocation = MapManager.Instance.currentMap.FindEntrance();
            Destroy(gameObject);
        }
    }
}