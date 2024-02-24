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
            saveButton.RegisterCallback<ClickEvent>(Save);

            Button loadButton = inspector.Query<Button>(name: "LoadButton");
            loadButton.RegisterCallback<ClickEvent>(Save);

            return inspector;
        }

        private void Save(ClickEvent _) => SaveManager.Instance.Save();

        private void Load(ClickEvent _) => SaveManager.Instance.LoadLast();
    }
}

