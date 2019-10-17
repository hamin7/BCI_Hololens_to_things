using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPGeneration : MonoBehaviour {

	public GameObject UDPCommGameObject;

	public string DataStringMiddleWare = "UDP is real.";
    public string DataStringMatlab = "UDP is real.";

    public first firstSphere;

    void Start () {
       
		if (UDPCommGameObject == null) {
			Debug.Log ("ERR UDPGEN: UDPSender is required. Self-destructing.");
			Destroy (this);
		}

    }

    void Update () {

        //onSelect된 시점에 PC로 특정 값 보내줘야 함

        if (MarkerControl.onSelect) //Boundingbox 스크립트에서  Gaze 연산, 판별해서 정적 변수 onSelect에 bool값 할당
        {
            if((DataStringMiddleWare != null) && (DataStringMatlab != null))
            {

                if (MarkerControl.SelectedMarker == "Robot")
                {
                    DataStringMiddleWare = "HanYang#QR#1";
                    DataStringMatlab = "1";
                }
                else if (MarkerControl.SelectedMarker == "Hum")
                {
                    DataStringMiddleWare = "HanYang#QR#3";
                    DataStringMatlab = "3";
                }
                else if (MarkerControl.SelectedMarker == "Air")
                {
                    DataStringMiddleWare = "HanYang#QR#2";
                    DataStringMatlab = "2";
                }
                else if (MarkerControl.SelectedMarker == "AC")
                {
                    DataStringMiddleWare = "HanYang#QR#4";
                    DataStringMatlab = "4";
                }
                else if (MarkerControl.SelectedMarker == "Bulb")
                {
                    DataStringMiddleWare = "HanYang#QR#5";
                    DataStringMatlab = "5";
                }
                /*
                 * HM중요 - 기기 추가시 이런 형태로 else if 문단 하나 더 추가해주기
                else if (MarkerControl.SelectedMarker == "게임오브젝트 이름")
                {
                    DataString = "4";

                }
                */

                //DataString = "onSelect UDP value"; // 조율 후 수정해야할 값
                var dataBytesMiddleWare = System.Text.Encoding.UTF8.GetBytes(DataStringMiddleWare);
                var dataBytesMatlab = System.Text.Encoding.UTF8.GetBytes(DataStringMatlab);
                UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

                //IP 주소: 192.168.1.214  포트번호: 8054         트라이얼 받는 포트번호:  8053    홀로렌즈는 8052 포트 열어서 듣기
                //IP주소 왼쪽 컴 동적할당이라 ip 주소 계속 바뀜

                /*HM 중요  IP 주소 변경: UDP 값을 보내야하는 IP 주소와 포트번호가 바뀔경우 */
                //어떤 기기가 선택됐느냐에 따라 약속된 값을 매트랩과 미들웨어에 보내주는 코드
                //comm.SendUDPMessage(string IP주소, string 포트번호, );

#if !UNITY_EDITOR
                comm.SendUDPMessage("192.168.1.213", "8054", dataBytesMatlab); 
                comm.SendUDPMessage("192.168.1.37", "8053", dataBytesMiddleWare);

#endif

                MarkerControl.onSelect = false; //UDP값 한번만 보내도록
            }
        }

        if (first.checker_1 == 0 && first.checker_2 == 1)
        {
            DataStringMiddleWare = firstSphere.start;
            DataStringMatlab = "a";
            Debug.Log("Starting Point: " + firstSphere.start + "\nDataString: " + DataStringMiddleWare);

            if (DataStringMiddleWare != null)
            {
                // UTF-8 is real
                var dataBytesMiddleWare = System.Text.Encoding.UTF8.GetBytes(DataStringMiddleWare);
                // HM 추가함.
                var dataBytesMatlab = System.Text.Encoding.UTF8.GetBytes(DataStringMatlab);
                UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

                /*HM 중요 IP 주소 변경: UDP 값을 보내야하는 IP 주소와 포트번호가 바뀔경우 */
                //comm.SendUDPMessage(string IP주소, string 포트번호, );
                //매트랩에 시작 트리거 보내주기
#if !UNITY_EDITOR
			comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytesMiddleWare);
             comm.SendUDPMessage("192.168.1.213", "8054", dataBytesMatlab); 
#endif
            }
        }


        if (first.checker_1 == 1 && first.checker_2 == 0)
        {
            DataStringMiddleWare = firstSphere.end;
            //HM 추가함.
            DataStringMatlab = "b";
            Debug.Log("Ending Point: " + firstSphere.end + "\nDataString: " + DataStringMiddleWare);

            if (DataStringMiddleWare != null)
            {
                // UTF-8 is real
                var dataBytesMiddleWare = System.Text.Encoding.UTF8.GetBytes(DataStringMiddleWare);
                // HM 추가함.
                var dataBytesMatlab = System.Text.Encoding.UTF8.GetBytes(DataStringMatlab);
                UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication>();

                /*HM 중요 IP 주소 변경: UDP 값을 보내야하는 IP 주소와 포트번호가 바뀔경우 */
                //comm.SendUDPMessage(string IP주소, string 포트번호, );
                //매트랩에 끝 트리거 보내주기
#if !UNITY_EDITOR
			comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytesMiddleWare);
            comm.SendUDPMessage("192.168.1.213", "8054", dataBytesMatlab); 
#endif
            }
        }


    }
}
//ORIGINAL CODE in Update()
/*if (DataString != null) {
    // UTF-8 is real
    var dataBytes = System.Text.Encoding.UTF8.GetBytes(DataString);
    UDPCommunication comm = UDPCommGameObject.GetComponent<UDPCommunication> ();

    // #if is required because SendUDPMessage() is async
    #if !UNITY_EDITOR
    comm.SendUDPMessage(comm.externalIP, comm.externalPort, dataBytes);
    #endif
}*/
