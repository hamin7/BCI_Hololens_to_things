using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUDP : MonoBehaviour {

    public GameObject UDPCommGameObject;

    public string StartTriggerMiddleWare = "HololenseHY#Star#01#Start";
    public string EndTriggerMiddleWare = "HololenseHY#Star#01#End";
    public string StartTriggerMatlab = "a";
    public string EndTriggerMatlab = "b";

    void Start()
    {
        if (UDPCommGameObject == null)
        {
            Debug.Log("ERR UDPGEN: UDPSender is required. Self-destructing.");
            Destroy(this);
        }
    }


    void Update()
    {
        if ((Star_1.checker_1 == 0 && Star_1.checker_2 == 1))
        {
            Debug.Log("Starting Point: " + StartTriggerMiddleWare);

            if ((StartTriggerMiddleWare != null)&&(StartTriggerMatlab != null))
            {
                // UTF-8 is real
                var dataBytesMiddleWare = System.Text.Encoding.UTF8.GetBytes(StartTriggerMiddleWare);
                // HM 추가함.
                var dataBytesMatlab = System.Text.Encoding.UTF8.GetBytes(StartTriggerMatlab);
                UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

                // #if is required because SendUDPMessage() is async
#if !UNITY_EDITOR
            comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytesMiddleWare); 
			comm.SendUDPMessage("1255.255.255.255", "8054", dataBytesMatlab); 
#endif
            }
        }


        if ((Star_1.checker_1 == 1 && Star_1.checker_2 == 0) )
        {
            Debug.Log("Ending Point: " + EndTriggerMiddleWare);

            if((EndTriggerMiddleWare != null)&&(EndTriggerMatlab != null))
            {
                // UTF-8 is real
                var dataBytesMiddleWare = System.Text.Encoding.UTF8.GetBytes(EndTriggerMiddleWare);
                // HM 추가함.
                var dataBytesMatlab = System.Text.Encoding.UTF8.GetBytes(EndTriggerMatlab);
                UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

                // #if is required because SendUDPMessage() is async
#if !UNITY_EDITOR
            comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytesMiddleWare); 
			comm.SendUDPMessage("255.255.255.255", "8054", dataBytesMatlab);
#endif
            }
        }


        if(EyeWritingControl.checker_1 == 0 && EyeWritingControl.checker_2 == 1)
        {
            Debug.Log("Icon Down");

            var dataBytes = System.Text.Encoding.UTF8.GetBytes("z");
            UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

            // #if is required because SendUDPMessage() is async
#if !UNITY_EDITOR
			comm.SendUDPMessage("255.255.255.255", "8054", dataBytes);
#endif
        }

    }


        /* ORIGINAL
         * 
         * if (DataString != null) {
			// UTF-8 is real
			var dataBytes = System.Text.Encoding.UTF8.GetBytes(DataString);
			UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication> ();

			// #if is required because SendUDPMessage() is async
			#if !UNITY_EDITOR
			comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytes);
			#endif
		}*/

    
}
