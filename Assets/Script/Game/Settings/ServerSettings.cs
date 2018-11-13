using UnityEngine;

namespace Game.Settings
{
    public class ServerSettings : ScriptableObject
    {
        public string RoomName = "default";
        public byte MaxPlayers = 2;
        public string LobbyName = "Lobby";
    }
}
