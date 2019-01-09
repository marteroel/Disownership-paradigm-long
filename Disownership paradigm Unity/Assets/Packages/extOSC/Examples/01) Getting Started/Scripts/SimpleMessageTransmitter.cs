/* Copyright (c) 2018 ExT (V.Sigalkin) */

using UnityEngine;

namespace extOSC.Examples
{
    public class SimpleMessageTransmitter : MonoBehaviour
    {
      
        public string Address = "/example/1";

        [Header("OSC Settings")]
        public OSCTransmitter Transmitter;

        

        protected virtual void Start()
        {
            var message = new OSCMessage(Address);
            message.AddValue(OSCValue.String("Hello, world!"));

            Transmitter.Send(message);
        }
			
    }
}