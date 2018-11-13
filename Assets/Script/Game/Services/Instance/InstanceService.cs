using Core.Binder;
using Core.UnityUtils;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Instance
{
    public class InstanceService : IInstanceService, IOnEventCallback
    {
        public event Action OnFinishRound;
        public event Action OnStartRound;
        public event Action OnFinishGame;

        private Dictionary<string, string> _map = new Dictionary<string, string>();

        public void StartGame()
        {
            PhotonNetwork.AddCallbackTarget(this);
            IQuizService quizService = BindManager.GetInstance<IQuizService>();
            quizService.Init();
        }

        public void PlayerAnswer(string data)
        {
            //IPlayerService playerService = BindManager.GetInstance<IPlayerService>();

            //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            //SendOptions sendOptions = new SendOptions { Reliability = true };

            //PhotonNetwork.RaiseEvent(NetworkEvents.PlayerAnswer, new string[] { playerService.UserName, data }, raiseEventOptions, sendOptions);
        }

        public QuizData GetNextQuiz()
        {
            IQuizService quizService = BindManager.GetInstance<IQuizService>();
            QuizData data = quizService.GetMockQuiz();
            if(data == null)
            {
                FinishGame();
                return null;
            }

            return data;
        }

        public void OnEvent(EventData photonEvent)
        {
            //if (photonEvent.Code == NetworkEvents.PlayerAnswer)
            //{
            //    if (PhotonNetwork.IsMasterClient)
            //    {
            //        string[] data = (string[])photonEvent.CustomData;
            //        RegisterAnswer(data);
            //    }

            //    return;
            //}

            //if (photonEvent.Code == NetworkEvents.FinishRound)
            //{
            //    if(OnFinishRound != null)
            //    {
            //        OnFinishRound();
            //    }

            //    return;
            //}

            //if (photonEvent.Code == NetworkEvents.StartRaund)
            //{
            //    if (OnStartRound != null)
            //    {
            //        OnStartRound();
            //    }

            //    return;
            //}

            //if (photonEvent.Code == NetworkEvents.FinishGame)
            //{
            //    PhotonNetwork.RemoveCallbackTarget(this);

            //    if (OnFinishGame != null)
            //    {
            //        OnFinishGame();
            //    }

            //    return;
            //}
        }

        private void RegisterAnswer(string[] data)
        {
            if (_map.ContainsKey(data[0]))
            {
                return;
            }

            _map[data[0]] = data[1];

            if(_map.Count >= PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                FinishRaund();
                CoroutineHelper.Instance.StartCoroutine(NewRaundCoroutine());
            }
        }

        private void FinishGame()
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            //    SendOptions sendOptions = new SendOptions { Reliability = true };

            //    PhotonNetwork.RaiseEvent(NetworkEvents.FinishGame, null, raiseEventOptions, sendOptions);
            //}
        }

        private void FinishRaund()
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            //    SendOptions sendOptions = new SendOptions { Reliability = true };

            //    PhotonNetwork.RaiseEvent(NetworkEvents.FinishRound, null, raiseEventOptions, sendOptions);
            //    _map.Clear();
            //}
        }

        private IEnumerator NewRaundCoroutine()
        {
            yield return new WaitForSeconds(1);
            StartNewRaund();
        }

        private void StartNewRaund()
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            //    SendOptions sendOptions = new SendOptions { Reliability = true };

            //    PhotonNetwork.RaiseEvent(NetworkEvents.StartRaund, null, raiseEventOptions, sendOptions);
            //}
        }
    }
}
