using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace extOSC.Examples
{
    public class InputController : MonoBehaviour
    {
        #region Public Vars

        public string Address = "/example/1";

        [Header("OSC Settings")]
        public OSCTransmitter Transmitter;

        #endregion

        #region Unity Methods



        protected virtual void Start()
        {
            var message = new OSCMessage(Address);
            message.AddValue(OSCValue.String("Hello, world!"));

            Transmitter.Send(message);
        }

        #endregion
        private void Update()
        {
            // check input
            if(Input.GetKeyDown("space"))
            {
                print("space pressed");
                string newAddress = "/vkb_midi/1/note/70";
                var message = new OSCMessage(newAddress);
                message.AddValue(OSCValue.Int(1));
                Transmitter.Send(message);
            }
            else if(Input.GetKeyUp("space"))
            {
                print("space released");
                string newAddress = "i/vkb_midi/1/note/70";
                var message = new OSCMessage(newAddress);
                message.AddValue(OSCValue.Int(0));
                Transmitter.Send(message);
            }
            // pitch bend wheel
            float newVal = Input.GetAxis("Horizontal");
            newVal *= 100;
            print("new val: " + newVal);
            string pitchAddress = "f/vkb_midi/1/cc/80";
            var pitchMessage = new OSCMessage(pitchAddress);
            pitchMessage.AddValue(OSCValue.Float(newVal));
            Transmitter.Send(pitchMessage);



        }
    }
}

