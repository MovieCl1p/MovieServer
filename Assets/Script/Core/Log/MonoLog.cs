using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class MonoLog
    {
        public static List<MonoLogChannel> Channels
        {
            get;
            private set;
        }

        static MonoLog()
        {
            Channels = new List<MonoLogChannel>();

            //			Channels.Add(MonoLogChannel.UI);
            //			Channels.Add(MonoLogChannel.Controllers);
            //			Channels.Add(MonoLogChannel.Core);
            Channels.Add(MonoLogChannel.All);
        }

        public static void Log(MonoLogChannel channel, object message)
        {
            if (Channels.Contains(channel) || Channels.Contains(MonoLogChannel.All))
            {
                string details = "[" + System.DateTime.UtcNow.ToString("HH:mm:ss.fff") + "] [" + channel.ToString() + "]: " + message.ToString();
                Debug.Log(details);
            }
        }

        public static void LogWarning(MonoLogChannel channel, object message)
        {
            if (Channels.Contains(channel) || Channels.Contains(MonoLogChannel.All))
            {
                Debug.LogWarning("Game [" + channel.ToString() + "]: " + message.ToString());
            }
        }

        public static void Log(MonoLogChannel channel, object message, Exception exception)
        {
            if (Channels.Contains(channel) || Channels.Contains(MonoLogChannel.Exceptions) || Channels.Contains(MonoLogChannel.All))
            {
                Debug.LogException(new Exception("Game [" + channel.ToString() + "]: " + message.ToString(), exception));
            }
        }
    }
}
