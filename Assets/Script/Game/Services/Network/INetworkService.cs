using Photon.Realtime;
using System;

namespace Game.Services
{
    public interface INetworkService
    {
        event Action<Player> OnPlayerJoinRoom;
        event Action<int> OnRoomCountdown;

        void CreateRoom();

        void Disconnect();

        void Init();
    }
}