
using Core.Binder;
using Core.Commands;
using Core.ViewManager;
using Core.ViewManager.Data;
using Game.Data;
using Game.Services;

namespace Game.Command
{
    public class StartCommand : ICommand
    {
        public void Execute()
        {
            INetworkService network = BindManager.GetInstance<INetworkService>();
            network.Init();

            IQuizService quizService = BindManager.GetInstance<IQuizService>();
            quizService.Init();

            RegisterView();
            
            ViewManager.Instance.SetView(ViewNames.SplashView);
        }

        private void RegisterView()
        {
            ViewManager.Instance.RegisterView(ViewNames.SplashView, LayerNames.ScreenLayer);
            ViewManager.Instance.RegisterView(ViewNames.MainMenu, LayerNames.ScreenLayer);
            ViewManager.Instance.RegisterView(ViewNames.GameView, LayerNames.ScreenLayer);

            ViewManager.Instance.RegisterView(ViewNames.RoomMenuView, LayerNames.WindowLayer);
            
        }
    }
}