
namespace Core.ViewManager
{
    public class BaseView : MonoScheduledBehaviour
    {
        protected bool IsRemoving;

        public object Options { get; set; }

        public ViewLayer Layer { get; set; }
        

        public void CloseView()
        {
            if (IsRemoving)
            {
                return;
            }

            IsRemoving = true;
            ViewManager.Instance.RemoveView(this);
        }
    }
}
