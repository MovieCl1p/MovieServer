using Core.ViewManager;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.RoomMenu
{
    public class RoomPlayerView : BaseView
    {
        [SerializeField]
        private Text _name;

        public void UpdateView(Player player)
        {
            _name.text = player.UserId;
        }
    }
}
