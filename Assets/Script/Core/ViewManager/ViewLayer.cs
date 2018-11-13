using System.Collections.Generic;

namespace Core.ViewManager
{
    public class ViewLayer : BaseMonoBehaviour
    {
        public BaseView Current { get; set; }

        private List<View> _inLineViews = new List<View>();

        public void AddView(BaseView view)
        {
            Current = view;
        }

        public void RemoveCurrentView()
        {
            if (Current != null)
            {
                Destroy(Current.gameObject);
                Current = null;
            }
        }

        public View GetNextView()
        {
            if (_inLineViews.Count > 0)
            {
                View lastView = _inLineViews[0];
                _inLineViews.Remove(lastView);
                return lastView;
            }
            else
            {
                return null;
            }
        }

        public void AddViewToLine(string viewId, object options = null, int index = -1)
        {
            if (index == -1)
            {
                _inLineViews.Add(new View(viewId, options));
            }
            else
            {
                _inLineViews.Insert(index, new View(viewId, options));
            }
        }
    }
}
