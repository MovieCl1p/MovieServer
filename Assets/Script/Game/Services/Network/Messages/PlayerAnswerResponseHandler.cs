using Core.Services.Network.Messages;

namespace Game.Services.Network
{
    public class PlayerAnswerResponseHandler<T> : MessageResponseHandler<T> where T : IMessage
    {
        public override void Process(T o)
        {
            base.Process(o);

            
        }
    }
}
