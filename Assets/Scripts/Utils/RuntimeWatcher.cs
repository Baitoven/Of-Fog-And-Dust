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
            // Attend que SaveManager soit prêt
            yield return new WaitUntil(() => SaveManager.Instance != null);

            // Lance le tutoriel
            SaveManager.Instance.LoadTutorial();

            // Nettoyage
            //Destroy(gameObject);
            Debug.Log("pourt");
        }
    }
}