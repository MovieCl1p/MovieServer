using System;
using System.Collections;
using Core.Binder;
using Core.Commands;
using Core.Dispatcher;
using Core.States;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using Game.Services.Network;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] 
        private Button _startServerBtn;

        private INetworkService _network;

        protected override void Start()
        {
            base.Start();
            _startServerBtn.onClick.AddListener(OnPlayClick);
        }

        private void OnPlayClick()
        {
            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.AddListener(NetworkEventType.RoomCreated, OnRoomCreated);
            dispatcher.AddListener(NetworkEventType.StartGame, OnStartGame);

            _network = BindManager.GetInstance<INetworkService>();;
            _network.CreateRoom();

            HidePlayBtn();
        }

        private void OnStartGame()
        {
            ViewManager.Instance.CloseView(ViewNames.RoomMenuView);
            ViewManager.Instance.SetView(ViewNames.GameView);
        }

        private void HidePlayBtn()
        {
            _startServerBtn.gameObject.SetActive(false);
        }

        private void OnRoomCreated()
        {
            ViewManager.Instance.SetView(ViewNames.RoomMenuView);
        }

        protected override void OnReleaseResources()
        {
            _startServerBtn.onClick.RemoveListener(OnPlayClick);
            base.OnReleaseResources();
        }
    }
}