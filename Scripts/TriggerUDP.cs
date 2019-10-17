using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUDP : MonoBehaviour {

    public GameObject UDPCommGameObject;

    public string StartTrigger;
    public string EndTrigger;

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
            Debug.Log("Starting Point: " + StartTrigger);

            if (StartTrigger != null)
            {
                // UTF-8 is real
                var dataBytes = System.Text.Encoding.UTF8.GetBytes(StartTrigger);
                UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

                // #if is required because SendUDPMessage() is async
#if !UNITY_EDITOR
			comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytes); 
#endif
            }
        }


        if ((Star_1.checker_1 == 1 && Star_1.checker_2 == 0) )
        {
            Debug.Log("Ending Point: " + EndTrigger);

            if (EndTrigger != null)
            {
                // UTF-8 is real
                var dataBytes = System.Text.Encoding.UTF8.GetBytes(EndTrigger);
                UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

                // #if is required because SendUDPMessage() is async
#if !UNITY_EDITOR
			comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytes);
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
			comm.SendUDPMessage("192.168.1.213", "8054", dataBytes);
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
