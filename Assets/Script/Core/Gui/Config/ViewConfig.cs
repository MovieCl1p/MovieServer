using System;
using Core.ViewManager;

namespace Core.Gui.Config
{
    [Serializable]
    public class ViewConfig
    {
        public string Id;
        public BaseView View;

        public ViewConfig(BaseView baseView)
        {
            View = baseView;
            Id = baseView.name;
        }
        
    }
}