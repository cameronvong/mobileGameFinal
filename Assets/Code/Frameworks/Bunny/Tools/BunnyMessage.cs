using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny.Tools
{
    public enum BunnyMessagePriority
    {
        LOW,
        MEDIUM,
        HIGH
    }

    public class BunnyMessage<T>
    {
        public T payload;
        public object sender;

        public BunnyMessage()
        {
            payload = default(T);
            sender = BunnyEventManager.Instance;
        }

        public BunnyMessage(
            T value,
            object source
        ) 
        {
            payload  = value;
            sender = source;
        }
    }
}