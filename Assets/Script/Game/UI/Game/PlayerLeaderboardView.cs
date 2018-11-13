using Core.ViewManager;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Game
{
    public class PlayerLeaderboardView : BaseView
    {
        [SerializeField]
        private Text _nameLabel;

        public void UpdateView(Player player)
        {
            _nameLabel.text = player.NickName;
        }
    }
}
