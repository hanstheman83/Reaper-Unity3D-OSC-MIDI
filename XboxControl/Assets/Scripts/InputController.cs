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
            ProcessInput();

            #region old code
            // check input
            //if (Input.GetKeyDown("space"))
            //{
            //    print("space pressed");
            //    string newAddress = "/vkb_midi/1/note/70";
            //    var message = new OSCMessage(newAddress);
            //    message.AddValue(OSCValue.Int(1));
            //    Transmitter.Send(message);
            //}
            //else if (Input.GetKeyUp("space"))
            //{
            //    print("space released");
            //    string newAddress = "i/vkb_midi/1/note/70";
            //    var message = new OSCMessage(newAddress);
            //    message.AddValue(OSCValue.Int(0));
            //    Transmitter.Send(message);
            //}

            // pitch bend wheel
            //float newVal = Input.GetAxis("Horizontal");
            //newVal *= 100;
            //print("new val: " + newVal);
            //string pitchAddress = "f/vkb_midi/1/cc/80";
            //var pitchMessage = new OSCMessage(pitchAddress);
            //pitchMessage.AddValue(OSCValue.Float(newVal));
            //Transmitter.Send(pitchMessage);

            // -1 to 1
            //Debug.Log("Joy 1 horizontal : " + Input.GetAxis("Horizontal"));
            //Debug.Log("Joy 1 vertical : " + Input.GetAxis("Vertical"));
            //Debug.Log("Joy 2 horizontal : " + Input.GetAxis("Horizontal2"));
            //Debug.Log("Joy 2 vertical : " + Input.GetAxis("Vertical2"));
            //// remap to midi cc, 0 to 127 : + 1 , * 64 , + 64 
            //float joy1Horizontal = (Input.GetAxis("Horizontal") + 1) * 64; // TODO now 128 max ?
            //Debug.Log("Joy 1 horizontal in midi : " + joy1Horizontal);
            //string newCCAddress = "f/vkb_midi/1/cc/80";
            //var joy1HorizontalMessage = new OSCMessage(newCCAddress);
            //joy1HorizontalMessage.AddValue(OSCValue.Float(joy1Horizontal));
            //Transmitter.Send(joy1HorizontalMessage);

            // only send midi cc if change!!

            /* # t: trigger or toggle message. The device triggers the action, or toggles the
            # state, when the pattern is sent with no arguments, or with an argument of 1.
            # The feedback values REAPER sends are identical to those sent for binary 
            # arguments.
            */
            //string newOSCAddress = "/play";
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    Transmitter.Send(new OSCMessage(newOSCAddress));
            //}
            #endregion


        }

        private void ProcessInput() // slow down to 100ms, gather data in 1 package
        {
            StartCoroutine(ProcessTriggers());
            ProcessShoulderBtns();
           

            // play and stop on cursor OR stop and rewind
            // Left/Right Shoulder

            // zoom in/out, 
            // Left Thumbstick

            // track down/up,  
            // DPad up/down
            // select previous/next mediaclip
            // DPad left/right


            // btn X
            // cut at cursor (and selected track)

            IEnumerator ProcessTriggers() // TODO add to package
            {
                // TODO control speed by amount of calls per second - and 1ms or 5ms OR by beat or by measure (set in GUI)

                // only want 1 active at a time : left dominant
                float leftTrigger = Input.GetAxis("TriggerLeft_Axis");
                float rightTrigger = Input.GetAxis("TriggerRight_Axis");
                string Address = "/action/str";
                var message = new OSCMessage(Address);

                // max = -1 , min = 0
                if(leftTrigger < 0) 
                {
                    print("left trigger : " + leftTrigger);
                    // rewind, frame independent
                    // Action call
                    
                    if(leftTrigger > -.25)
                    {
                        message.AddValue(OSCValue.String("_SWS_MOVECUR1MSLEFT"));

                    }
                    else if(leftTrigger > -.5)
                    {
                        message.AddValue(OSCValue.String("_SWS_MOVECUR5MSLEFT"));
                    }
                    else if(leftTrigger > -.75)
                    {
                        //Move edit cursor back one beat
                        // add timeout
                        message.AddValue(OSCValue.String("41045"));

                    }
                }
                else if(rightTrigger < 0)
                {
                    print("right trigger : " + rightTrigger);
                }


                Transmitter.Send(message);
                yield return new WaitForSeconds(.5f);

            }






            void ProcessShoulderBtns()
            {


            }

        }
    }
}