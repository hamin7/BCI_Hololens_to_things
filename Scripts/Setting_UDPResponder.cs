using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_UDPResponder : MonoBehaviour {

    public string EyeBlink4Times;

    public string[] MethodSelect; // QR or Eye or SSVEP // IntroScene

    //public string[] ApplianceSelect; // 기기선택. QR, Eye, SSVEP 중 어떤 방식을 선택했느냐에 따라 기기 가짓수가 다름

    public string[] CommandSelect; // 네개의 SSVEP 자극 중 1,2,3번 자극은 기기별 명령어. 4번 자극은 기기선택단계로 돌아감.

    public GameObject StimuliSet;

    public bool MethodStimuli = false;

    public bool CommandStimuli = false;

    private void Awake()
    {
        if(/*MethodStimuli ||*/ CommandStimuli)
            StimuliSet.SetActive(false);
        
        ForTest_UDPresponder.EyeBlink4Times = EyeBlink4Times;
        ForTest_UDPresponder.MethodSelect = MethodSelect;

        ForTest_UDPresponder.CommandSelect = CommandSelect;

        ForTest_UDPresponder.StimuliSet = StimuliSet;

        
    }
    
}
