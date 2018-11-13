using Game.Command;
using Game.Config;
using UnityEngine;

namespace Game
{
    public class AppRoot : MonoBehaviour
    {
        private void Awake()
        {
            AppConfig config = new AppConfig();
            config.Init();
            
            StartCommand command = new StartCommand();
            command.Execute();
        }
    }
}
