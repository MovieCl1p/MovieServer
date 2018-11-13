using Core.Binder;
using Core.Dispatcher;
using Game.Services;
using Game.Services.Instance;
using Game.Services.Network;

namespace Game.Config
{
    public class AppConfig
    {
        public void Init()
        {
            BindServices();
        }

        private void BindServices()
        {
            BindManager.Bind<IDispatcher>().To<Dispatcher>().ToSingleton(); 

            BindManager.Bind<INetworkService>().To<NetworkService>().ToSingleton();
            BindManager.Bind<IQuizService>().To<QuizService>().ToSingleton(); 
            BindManager.Bind<IInstanceService>().To<InstanceService>().ToSingleton();
        }
    }
}
