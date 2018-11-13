using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Services.Network
{
    public static class MessageId
    {
        public static readonly byte None = 0;

        public static readonly byte RoomReady = 7;
        public static readonly byte RoomCountDown = 8;

        public static readonly byte GoToGame = 9;

        public static readonly byte Answer = 10;
    }
}
