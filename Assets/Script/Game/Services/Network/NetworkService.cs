using System;
using Core.UnityUtils;
using ExitGames.Client.Photon;
using Core.Services.Network.Specifications;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Core.Services.Network.Messages;
using Core;
using System.Collections;
using Core.States;
using Core.Dispatcher;
using Core.Binder;

namespace Game.Services.Network
{
    public class NetworkService : INetworkService, IOnEventCallback
    {
        public event Action<int> OnRoomCountdown;
        public event Action<Player> OnPlayerJoinRoom;

        private PhotonManager _photon;

        public void Init()
        {
            _photon = PhotonManager.Instance;
            _photon.OnRoomCreated += RoomCreated;
            _photon.OnRoomReady += OnRoomReady;
            _photon.OnPlayerJoinedRoom += OnPlayerJoinedRoom;
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void RoomCreated()
        {
            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.Dispatch(NetworkEventType.RoomCreated);
        }

        public void CreateRoom()
        {
            _photon.ConnectToPhoton();
        }

        public void Disconnect()
        {
            _photon.Disconnect();
        }

        public void OnEvent(EventData photonEvent)
        {
            MonoLog.Log(MonoLogChannel.MultiPlayer, "photonEvent: " + photonEvent.ToString());

            IMessage message = new Message(photonEvent);
            IMessageResponseHandler<IMessage> root = new MessageResponseHandler<IMessage>();
            IMessageResponseHandler<IMessage> last = new MessageResponseHandler<IMessage>();
            IMessageResponseHandler<IMessage> answer = new PlayerAnswerResponseHandler<IMessage>();

            ISpecification<IMessage> rootSpec = new Specification<IMessage>(o => o.MessageId == MessageId.None);
            ISpecification<IMessage> answerSpec = new Specification<IMessage>(o => o.MessageId == MessageId.Answer);
            ISpecification<IMessage> lastSpec = new Specification<IMessage>(o => true);

            root.SetSpecification(rootSpec);
            answer.SetSpecification(answerSpec);
            last.SetSpecification(lastSpec);

            root.SetSuccessor(answer);
            answer.SetSuccessor(last);

            root.HandleRequest(message);
        }

        private void OnPlayerJoinedRoom(Player player)
        {
            if(OnPlayerJoinRoom != null)
            {
                OnPlayerJoinRoom(player);
            }
        }

        private void OnRoomReady()
        {
            UpdateNotifier.Instance.ExecuteWithDelay<int>(CountdownTick, 5, 1.0f);
        }

        private void CountdownTick(int secondsLeft)
        {
            if(OnRoomCountdown != null)
            {
                OnRoomCountdown(secondsLeft);
            }

            if (secondsLeft <= 0)
            {
                RaiseEventOptions reOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
                SendOptions options = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(MessageId.GoToGame, null, reOptions, options);

                IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
                dispatcher.Dispatch(NetworkEventType.StartGame);

                return;
            }

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            SendOptions sendOptions = new SendOptions { Reliability = true };

            PhotonNetwork.RaiseEvent(MessageId.RoomCountDown, secondsLeft, raiseEventOptions, sendOptions);

            UpdateNotifier.Instance.ExecuteWithDelay<int>(CountdownTick, --secondsLeft, 1.0f);
        }
    }
}