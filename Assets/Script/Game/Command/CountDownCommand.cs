using System;
using Core.Commands;
using Core.UnityUtils;
using UnityEngine;

namespace Game.Command
{
    public class CountDownCommand : IUpdateHandler, ICommand
    {
        private float _count;
        private float _delay;

        public CountDownCommand(float delay)
        {
            _delay = delay;

            UpdateNotifier.Instance.Register(this);
            IsUpdating = false;
            //PhotonNetwork.OnEventCall += OnEventCall;

        }

        public bool IsUpdating { get; set; }

        public bool IsRegistered { get; set; }

        public void Execute()
        {
            IsUpdating = true;
        }

        public void OnUpdate()
        {
            _count += Time.deltaTime;
            if(_count >= _delay)
            {
                FinishCommand();
            }
        }

        private void FinishCommand()
        {
            
        }

        private void OnEventCall(byte eventCode, object content, int senderId)
        {
            if (eventCode == 7)
            {
                
                return;
            }
        }
    }
}
