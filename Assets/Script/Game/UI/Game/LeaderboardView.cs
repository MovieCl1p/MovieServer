using Core.ViewManager;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Game
{
    public class LeaderboardView : BaseView
    {
        [SerializeField]
        private PlayerLeaderboardView _prefab;

        [SerializeField]
        private Transform _content;

        private List<PlayerLeaderboardView> _list;

        public void UpdateView(List<Player> players)
        {
            Clear();

            for (int i = 0; i < players.Count; i++)
            {
                PlayerLeaderboardView view = Instantiate(_prefab, _content);
                view.UpdateView(players[i]);
                _list.Add(view);
            }
        }

        public void Clear()
        {
            for (int i = _list.Count - 1; i >= 0; i--)
            {
                Destroy(_list[i].gameObject);
            }

            _list.Clear();
        }
    }
}
