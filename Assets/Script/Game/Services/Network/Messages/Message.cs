using Core.Services.Network.Messages;
using ExitGames.Client.Photon;

namespace Game.Services.Network
{
    public class Message : IMessage
    {
        private EventData photonEvent;

        public byte MessageId
        {
            get
            {
                return photonEvent.Code;
            }
        }

        public Message(EventData photonEvent)
        {
            this.photonEvent = photonEvent;
        }
    }
}
