using System;
using System.Collections.Generic;
using Core.Gui.Config;
using Core.ViewManager;
using UnityEngine;

namespace Core.ResourceManager
{
    public static class ResourcesCache
    {
        private const string UiConfigPath = "Configs/UIConfig";
        
        private static Dictionary<string, BaseView> _map = new Dictionary<string, BaseView>();

        private static UIConfig _uiConfig;
        
        private static void Initialize()
        {
            if (_uiConfig == null)
            {
                _uiConfig = Resources.Load<UIConfig>(UiConfigPath);
                if (_uiConfig == null)
                {
                    Debug.LogError("No UIConfig by path: " + UiConfigPath);
                    return;
                }
                
                for (int i = 0; i < _uiConfig.Views.Count; i++)
                {
                    _map.Add(_uiConfig.Views[i].Id, _uiConfig.Views[i].View);
                }
            }
        }

        public static BaseView GetViewById(string viewId)
        {
            Initialize();
            
            if (!_map.ContainsKey(viewId))
            {
                Debug.LogError("No view found with Id: " + viewId);
                return null;
            }
            
            return _map[viewId];
        }

        public static T GetConfig<T>(string path) where T : UnityEngine.Object
        {
            T result = Resources.Load<T>(path);
            return result;
        }
    }
}
