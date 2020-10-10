using System.Collections;
using System.Collections.Generic;
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
            if (Input.GetKeyDown("space"))
            {
                print("space pressed");
                string newAddress = "/vkb_midi/1/note/70";
                var message = new OSCMessage(newAddress);
                message.AddValue(OSCValue.Int(1));
                Transmitter.Send(message);
            }
            else if (Input.GetKeyUp("space"))
            {
                print("space released");
                string newAddress = "i/vkb_midi/1/note/70";
                var message = new OSCMessage(newAddress);
                message.AddValue(OSCValue.Int(0));
                Transmitter.Send(message);
            }

            // pitch bend wheel
            //float newVal = Input.GetAxis("Horizontal");
            //newVal *= 100;
            //print("new val: " + newVal);
            //string pitchAddress = "f/vkb_midi/1/cc/80";
            //var pitchMessage = new OSCMessage(pitchAddress);
            //pitchMessage.AddValue(OSCValue.Float(newVal));
            //Transmitter.Send(pitchMessage);

            // -1 to 1
            Debug.Log("Joy 1 horizontal : " + Input.GetAxis("Horizontal"));
            Debug.Log("Joy 1 vertical : " + Input.GetAxis("Vertical"));
            Debug.Log("Joy 2 horizontal : " + Input.GetAxis("Horizontal2"));
            Debug.Log("Joy 2 vertical : " + Input.GetAxis("Vertical2"));
            // remap to midi cc, 0 to 127 : + 1 , * 64 , + 64 
            float joy1Horizontal = (Input.GetAxis("Horizontal") + 1) * 64; // TODO now 128 max ?
            Debug.Log("Joy 1 horizontal in midi : " + joy1Horizontal);
            string newCCAddress = "f/vkb_midi/1/cc/80";
            var joy1HorizontalMessage = new OSCMessage(newCCAddress);
            joy1HorizontalMessage.AddValue(OSCValue.Float(joy1Horizontal));
            Transmitter.Send(joy1HorizontalMessage);

            // only send midi cc if change!!

            /* # t: trigger or toggle message. The device triggers the action, or toggles the
            # state, when the pattern is sent with no arguments, or with an argument of 1.
            # The feedback values REAPER sends are identical to those sent for binary 
            # arguments.
            */
            string newOSCAddress = "/play";
            if (Input.GetButtonDown("Fire1"))
            {
                Transmitter.Send(new OSCMessage(newOSCAddress));
            }



        }
    }
}