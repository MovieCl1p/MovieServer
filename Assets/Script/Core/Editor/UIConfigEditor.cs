using System.IO;
using Core.Gui.Config;
using Core.ViewManager;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    [CustomEditor(typeof(UIConfig))]
    public class UIConfigEditor : UnityEditor.Editor
    {
        [SerializeField]
        private string _path;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            UIConfig config = (UIConfig) target;
            _path = config.PathToAssets;
            
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(_path); 
                
                if (GUILayout.Button("Choose path"))
                {
                    ChoosePath(config);
                }
            }
            GUILayout.EndHorizontal();
        }

        private void ChoosePath(UIConfig config)
        {
            string folderPath = EditorUtility.OpenFolderPanel("Choose folder", "", "");
            if (string.IsNullOrEmpty(folderPath))
            {
                return;
            }
            
            _path = folderPath.Replace(Application.dataPath, "Assets");
            
            
            config.Views.Clear();
            
            string[] assets = AssetDatabase.FindAssets("t: prefab", new []{_path});
            
            for (int i = 0; i < assets.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(assets[i]);
                BaseView view  = AssetDatabase.LoadAssetAtPath<BaseView>(path);
                if (view == null)
                {
                    continue;
                }
                
                config.Views.Add(new ViewConfig(view));
            }

            config.PathToAssets = _path;
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }
}