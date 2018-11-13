
namespace Core.ViewManager
{
    public interface IViewManager
    {
        void RegisterView(string viewId, string layerId);

        BaseView SetView(string viewId, object options = null);
        
    }
}
