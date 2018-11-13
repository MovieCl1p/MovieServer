using System;
using System.Collections.Generic;
using Core;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game.Services.Network
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        public event Action OnRoomCreated;
        public event Action OnRoomReady;
        public event Action<Player> OnPlayerJoinedRoom;

        [SerializeField]
        private Game.Settings.ServerSettings _serverSettings;

        private string _gameVersion = "1";

        private static PhotonManager _instance;

        public static PhotonManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void ConnectToPhoton()
        {
            if (PhotonNetwork.IsConnected)
            {
                JoinRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = _gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            MonoLog.Log(MonoLogChannel.MultiPlayer, "OnConnectedToMaster: Next -> try to Join Random Room");
            JoinRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            MonoLog.Log(MonoLogChannel.MultiPlayer, "OnPlayerEnteredRoom: " + newPlayer.NickName);

            if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
                SendOptions sendOptions = new SendOptions { Reliability = true };

                PhotonNetwork.RaiseEvent(MessageId.RoomReady, null, raiseEventOptions, sendOptions);

                if(OnRoomReady != null)
                {
                    OnRoomReady();
                }
            }

            if(OnPlayerJoinedRoom != null)
            {
                OnPlayerJoinedRoom(newPlayer);
            }
        }

        private void JoinRoom()
        {
            TypedLobby lobby = new TypedLobby(_serverSettings.LobbyName, LobbyType.Default);
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = _serverSettings.MaxPlayers;
            options.PublishUserId = true;
            PhotonNetwork.JoinOrCreateRoom(_serverSettings.RoomName, options, lobby);
        }

        public override void OnJoinedRoom()
        {
            MonoLog.Log(MonoLogChannel.MultiPlayer, "OnJoinedRoom");
            if(OnRoomCreated != null)
            {
                OnRoomCreated();
            }
        }

        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            MonoLog.Log(MonoLogChannel.MultiPlayer, "OnDisconnected: " + cause);
        }

    }
}