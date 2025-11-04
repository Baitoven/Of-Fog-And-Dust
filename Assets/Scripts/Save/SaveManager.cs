using Newtonsoft.Json;
using OfFogAndDust.Map;
using OfFogAndDust.Save.Types;
using System;
using System.IO;
using System.Text;
using UnityEngine;

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
                map = MapManager.Instance.currentMap,
            };
            if (File.Exists(Path.Join(Application.streamingAssetsPath, "maps/save.json")))
            {
                File.Delete(Path.Join(Application.streamingAssetsPath, "maps/save.json"));
            }
            FileStream stream = File.Create(Path.Join(Application.streamingAssetsPath, "maps/save.json"));
            string json = JsonConvert.SerializeObject(content);
            stream.Write(Encoding.UTF8.GetBytes(json), 0, Encoding.UTF8.GetByteCount(json));
        }

        private void Load(string saveFileName)
        {
            FileStream stream = File.OpenRead(Path.Join(Application.streamingAssetsPath, saveFileName));
            Byte[] content = new byte[stream.Length];
            stream.Read(content);
            TParsedSave save = JsonConvert.DeserializeObject<TParsedSave>(Encoding.UTF8.GetString(content));
            MapManager.Instance.LoadMap(save.map);
        }

        public void LoadLast()
        {
            Load("maps/save.json");
        }

        public void LoadTutorial()
        {
            Load("maps/tutorial.json");
        }
    }
}
