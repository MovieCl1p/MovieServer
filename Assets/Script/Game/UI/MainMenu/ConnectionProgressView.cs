using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.MainMenu
{
    public class ConnectionProgressView : MonoScheduledBehaviour
    {
        [SerializeField]
        private List<GameObject> _items;

        private int _currentIndex = 0;

        protected override void Start()
        {
            base.Start();

            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].SetActive(false);
            }
        }

        public void Activate(bool active)
        {
            if(active)
            {
                ScheduleUpdate(1, true);
                return;
            }

            UnscheduleUpdate();
        }

        protected override void OnScheduledUpdate()
        {
            base.OnScheduledUpdate();

            if(_currentIndex == 0)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    _items[i].SetActive(false);
                }
            }

            _items[_currentIndex].SetActive(true);
            if(++_currentIndex >= _items.Count)
            {
                _currentIndex = 0;
            }
        }
    }
}
