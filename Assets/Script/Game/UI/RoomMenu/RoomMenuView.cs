using System;
using Core.Binder;
using Core.Dispatcher;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using Game.Services.Network;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.RoomMenu
{
    public class RoomMenuView : BaseView
    {
        [SerializeField] 
        private Text _countDownLabel;
        
        [SerializeField] 
        private Text _text;

        [SerializeField]
        private RoomPlayerView _playerPrefab;

        [SerializeField]
        private Transform _list;

        private INetworkService _network;
        
        protected override void Start()
        {
            base.Start();

            _network = BindManager.GetInstance<INetworkService>();
            _network.OnPlayerJoinRoom += OnPlayerJoin;
            _network.OnRoomCountdown += OnRoomCountdown;

        }

        private void OnRoomCountdown(int value)
        {
            _countDownLabel.text = value.ToString();
        }

        private void OnPlayerJoin(Player player)
        {
            RoomPlayerView playerView = Instantiate(_playerPrefab, _list);
            playerView.UpdateView(player);
        }

        private void OnCountDown(int value)
        {
            _countDownLabel.text = value.ToString();
        }

        protected override void OnReleaseResources()
        {
            _network.OnPlayerJoinRoom -= OnPlayerJoin;
            _network.OnRoomCountdown -= OnRoomCountdown;
            base.OnReleaseResources();
        }
    }
}