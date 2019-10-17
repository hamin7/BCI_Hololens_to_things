using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeWritingControl : MonoBehaviour {

    //Trigger UDP - UDPGeneration 에 참조
    //패턴그림 
    //대기 시간, trial 길이 등을 안내해줄 UI

    public TextMesh CenterOfStimuli;
    public Light DirectionalLight;

    public static GameObject PatternIcon;
    //public static GameObject Spots;

    public static int checker_1, checker_2;

    public float IconDurationTime;

    bool inIconCoroutine = false;
    void Awake () {
        PatternIcon = transform.Find("Icon").gameObject;
        PatternIcon.SetActive(false); // 수정
        //Spots = transform.Find("Spots").gameObject;

        StartCoroutine(TrialControl());

        ForTest_UDPresponder.StimuliSet =  GameObject.Find("CommandStimuli");
        ForTest_UDPresponder.StimuliSet.SetActive(false);
    }
    private void Update()
    {
        if (PatternIcon.activeSelf && !inIconCoroutine) // 애니메이션이나 lerp 효과써서 rotation.x가 특정 값인걸로 판별할수도 있음
        {
            StartCoroutine(IconControl());
            
        }
    }
    IEnumerator IconControl()
    {
        inIconCoroutine = true;
        yield return new WaitForSeconds(IconDurationTime); //이 시간 다 지나고도 아이콘이 계속 떠있음

        //Icon 끝나는 시점에 특정 UDP값 BCI(차후에 MW로 )로 보내줘야함
        checker_1 = 0;
        checker_2 = 1;

        yield return new WaitForSeconds(0.01f);

        checker_1 = 1;
        checker_2 = 1;


        PatternIcon.SetActive(false);
        inIconCoroutine = false;
        StartCoroutine(TrialControl());

    }
    IEnumerator TrialControl()
    {
        //Trial 시작 전 3초 쉼
        //쉬는 타임에는 조명 살짝 어둡게
        DirectionalLight.color = Color.black;

        yield return new WaitForSeconds(0.2f);


        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);

        DirectionalLight.color = Color.white;


        /*
        // 5초 길이의 trial//

        //STARTING POINT    
        checker_1 = 0;
        checker_2 = 1;

        yield return new WaitForSeconds(0.01f);

        checker_1 = 1;
        checker_2 = 1;

        yield return new WaitForSeconds(5f);//trial 진행

        //ENDING POINT
        checker_1 = 1;
        checker_2 = 0;

        yield return new WaitForSeconds(0.01f);

        checker_1 = 0;
        checker_2 = 0;*/

    }

}
