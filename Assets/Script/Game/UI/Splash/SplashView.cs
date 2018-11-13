using Core.ViewManager;
using Game.Data;

namespace Game.UI.Splash
{
    public class SplashView : BaseView
    {
        protected override void Start()
        {
            base.Start();
            
            ScheduleUpdate(0.5f, false);
        }

        protected override void OnScheduledUpdate()
        {
            base.OnScheduledUpdate();

            ViewManager.Instance.SetView(ViewNames.MainMenu);
        }
    }
}