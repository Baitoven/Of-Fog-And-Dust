using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OfFogAndDust.Save
{
    [CustomEditor(typeof(SaveManager))]
    public class SaveManagerInspector : Editor
    {
        [SerializeField] private VisualTreeAsset _treeAsset;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();
            _treeAsset.CloneTree(inspector);

            Button saveButton = inspector.Query<Button>(name: "SaveButton");
            saveButton.clicked += Save;

            Button loadButton = inspector.Query<Button>(name: "LoadButton");
            loadButton.clicked += Load;

            return inspector;
        }

        private void Save() => SaveManager.Instance.Save();

        private void Load() => SaveManager.Instance.LoadLast();
    }
}

