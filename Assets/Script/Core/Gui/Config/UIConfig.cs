using System.Collections.Generic;
using Core.ViewManager;
using UnityEngine;

namespace Core.Gui.Config
{
    public class UIConfig : ScriptableObject
    {
        [SerializeField] 
        private List<ViewConfig> _views;

        [HideInInspector]
        [SerializeField] 
        private string _pathToAssets;

        public List<ViewConfig> Views
        {
            get { return _views; }
        }

        public string PathToAssets
        {
            get { return _pathToAssets; }
            set { _pathToAssets = value; }
        }
    }
}