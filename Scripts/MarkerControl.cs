using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerControl : MonoBehaviour {

    //1. system OFF (reset) -어플실행시 초기상태 또는 UDP 9 들어왔을때 : 마커 Icon 비활성화. StimuliParent 비활성화 
    //2. system ON   - UDP 9 들어왔을때 토글 : 마커 Icon 활성화. 
    //3. onSelect - Boundingbox 에서 게이즈 3초 판별 시점 : 마커 Icon 비활성화. StimuliParent 활성화
    //4. return- 기기 명령어 4번에 해당하는 UDP 값 들어왔을때 : 마커 Icon 활성화. StimuliParent 비활성화 

    public static GameObject GazeCursor;

    public static GameObject[] Markers; //세개의 마커 

    /* HM 중요 */
    public static string SelectedMarker = null; //Boundingbox에서 select처리된 마커의 이름  

    public static GameObject stimuliParent; //자극들의 root 부모 

    public static GameObject EyeWriting; //EyeWriting 의 Icon, Spots 부모객체

    public static bool isSystemOn; // ForTest_UDPresponder로 참조. 9 들어 왔을때  On(마커활성화)/Off(마커비활성화 및 초기화면)

    public static bool onSelect; //BoundBox(게이즈 응시 시간(프레임)카운트하여 select 판별)로 참조 -> UDPGeneration(참일때 UDP값 PC로 전송)

    public static int frameCount=0;

    void Start () {

        GazeCursor = GameObject.FindGameObjectWithTag("Respawn");

        Markers = GameObject.FindGameObjectsWithTag("Player");

        //마커 자체 비활성화/활성화 문제 있는듯. 대신에 boundingbox 컴포넌트가 포함된 Icon 참조
        for(int i = 0; i < 5; i++)
        {
            Markers[i] = Markers[i].transform.Find("Icon").gameObject;
        }

        //자극 객체 참조
        stimuliParent = GameObject.Find("StimuliParent");

        //자극 네개의 부모객체를 비활성화
        //에디터에서 활성화 되어 있는 자극은 root 부모로 활성화/비활성화 일괄로 할 수 있는듯- FPStext빼고 다 root 부모 따라서 활성화/비활성화됨
        stimuliParent.SetActive(false);

        //onSelect 기본 거짓값. 
        onSelect = false;

        /*
#if !UNITY_EDITOR
        //눈깜빡임 신호값 들어오기 전까지 기본 SystamOff 세팅

        if(ForTest_UDPresponder.isQRFirstStage){
            SystemOFF();
        }else{
                SystemON();
            ForTest_UDPresponder.isQRFirstStage = true;
        }
#endif


*/
#if UNITY_EDITOR
        SystemON();
#endif
#if !UNITY_EDITOR
        if (ForTest_UDPresponder.isQRFirstStage)
        {
            SystemOFF();
        }
        else
        {
            SystemOFF();
            SystemON();          
        }
#endif
    }


    //UDP 9 들어올 때 ForTest_UDPresponder에서 호출되는 system 토글 
    public static void SystemON()
    {
        ActivateMarker(true);
        GazeCursor.SetActive(true);
        frameCount = 0;
    }
    public static void SystemOFF()
    {
        ActivateMarker(false);
        stimuliParent.SetActive(false);
        GazeCursor.SetActive(false);
    }

    //마커, 자극 활성화 컨트롤
    //Boundingbox와 ForText_UDPresponder에서 참조
    public static void ActivateMarker(bool activate)
    {
        foreach (GameObject marker in Markers)
        {
            marker.SetActive(activate);
        }
    }

    //자극 네개의 부모객체 활성화 컨트롤
    public static void ActivateStimuli(bool activate)
    {

        stimuliParent.SetActive(activate);

    }

}
 //ADD eyemove//
    //SystemON 이후 5초 카운트후 eyewriting. 카운트 도중 마커 detect하면 카운트 중단 및 초기화. 마커 detect 없는 상태면 다시 카운트 0부터 시작
    /*private void Update()
    {
        //마커가 활성화 되기 전에는 5초 카운트 들어가면 안됨
        //모든 마커가 활성화 되어 있는지 체크
        foreach (GameObject marker in Markers)
        {
            if (!marker.activeSelf)
                return;
        }
      
        //모든 마커 활성화 && spriteRenderer가 비활성화되어있을때 5초 카운트
        if(!Markers[0].GetComponent<SpriteRenderer>().enabled&& !Markers[1].GetComponent<SpriteRenderer>().enabled && !Markers[2].GetComponent<SpriteRenderer>().enabled)
        {
            frameCount++;
            //Debug.Log("FrameCount : " + frameCount);
            if (frameCount == 300)
            {
                ActivateMarker(false);
                EyeWriting.transform.Find("Icon").gameObject.SetActive(true);
                GazeCursor.SetActive(false);
                frameCount++;
            }
        }//SpriteRenderer 하나라도 활성화되면 카운트 중지, frameCount 0으로 초기화
        else
        {
            frameCount = 0;
        }
    }*/