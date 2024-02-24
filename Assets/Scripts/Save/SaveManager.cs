using UnityEngine;
using System.IO;
using OfFogAndDust.Save.Types;
using System.Text;
using System;
using OfFogAndDust.Map;

namespace OfFogAndDust.Save
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        // TODO: savename as timestamp
        public void Save()
        {
            TParsedSave content = new TParsedSave
            {
                date = DateTime.Now,
                map = MapManager.Instance.currentMap
            };
            FileStream stream = File.Create(Path.Join(Application.streamingAssetsPath, "save"));
            string json = JsonUtility.ToJson(content);
            stream.Write(Encoding.UTF8.GetBytes(json), 0, Encoding.UTF8.GetByteCount(json));
        }

        public void LoadLast()
        {
            FileStream stream = File.OpenRead(Path.Join(Application.streamingAssetsPath, "save"));
            Byte[] content = new byte[stream.Length];
            stream.Read(content);
            TParsedSave save = JsonUtility.FromJson<TParsedSave>(Encoding.UTF8.GetString(content));
            MapManager.Instance.LoadMap(save.map);
        }
    }
}
