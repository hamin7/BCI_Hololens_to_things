using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_2 : MonoBehaviour {

    int currentScale;
    Transform GOtransform;
    SpriteRenderer GOspriterenderer;
    SpriteRenderer Icon;

    public int FrameCount;

    void Start() {
        currentScale = 0;

        GOtransform = gameObject.GetComponent<Transform>();
        GOspriterenderer = gameObject.GetComponent<SpriteRenderer>();

        /*HM 중요  */
        if (gameObject.name == "Star_4" || transform.root.name == "ApplianceStimuli")
            Icon = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        
        else if(ForTest_UDPresponder.ApplianceSelect == "MiddleWare#HanYang#SelectDevice#RVC" )//
        {
            Icon = transform.Find("RVC").transform.GetComponent<SpriteRenderer>();
        }
        else if (ForTest_UDPresponder.ApplianceSelect == "MiddleWare#HanYang#SelectDevice#AirCleaner#01")//
        {
            Icon = transform.Find("Air1").transform.GetComponent<SpriteRenderer>();
        }
        else if (ForTest_UDPresponder.ApplianceSelect == "MiddleWare#HanYang#SelectDevice#AirCleaner#02"  )//
        {
            Icon = transform.Find("Air2").transform.GetComponent<SpriteRenderer>();
        }
        else if (ForTest_UDPresponder.ApplianceSelect == "MiddleWare#HanYang#SelectDevice#AirConditioner")//
        {
            Icon = transform.Find("AC").transform.GetComponent<SpriteRenderer>();
        }
        else if (ForTest_UDPresponder.ApplianceSelect == "MiddleWare#HanYang#SelectDevice#Bulb")//
        {
            Icon = transform.Find("Bulb").transform.GetComponent<SpriteRenderer>();
        }
        else
        {
            Icon = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
            //Icon = transform.Find("Air2").transform.GetComponent<SpriteRenderer>();
            //Icon = transform.Find("RVC").transform.GetComponent<SpriteRenderer>();
            //Icon = transform.Find("Phone").transform.GetComponent<SpriteRenderer>();
        }

        Icon.color = new Color(0f, 0f, 0f, 1f);

        StartCoroutine(revisedSwitching());
    }
    private void OnEnable()
    {

        currentScale = 0;
        GOtransform.localScale = Star_1.Scales[currentScale];
        GOspriterenderer.color = Star_1.Colors[currentScale];
        Icon.color = Star_1.IconColors[currentScale];

        StartCoroutine(revisedSwitching());
    }

    IEnumerator revisedSwitching()
    {
        //첫 트라이얼만 3초 쉬고 시작
        if (Star_1.secCount == 0)
        {
            //if (gameObject.transform.root.name == "CommandStimuli")
                yield return new WaitForSeconds(3f);
        }
        else
        {
            yield return new WaitForSeconds(Star_1.TimeBetweenTrial);
        }

        while (true)
        {
            //yield return StartCoroutine(WaitFor.Frames(7));
            yield return StartCoroutine(WaitFor.Frames(FrameCount));

            //아이콘 크기를 Star_1와 별개로 지정해야한다면 이 스크립트에 새로운 배열 선언해서 사용하기 

            GOtransform.localScale = Star_1.Scales[currentScale];
            GOspriterenderer.color = Star_1.Colors[currentScale];
            Icon.color = Star_1.IconColors[currentScale];

            currentScale += 1;
            currentScale %= 2;

            if (currentScale == 1 && Star_1.tempTime - Star_1.secCount > Star_1.TrialDuration)
            {
                yield return new WaitForSecondsRealtime(Star_1.TimeBetweenTrial);
            }
        }
    }


    public static class WaitFor
    {
        public static IEnumerator Frames(int frameCount)
        {
            /*if (frameCount <= 0)
            {
                throw new ArgumentOutOfRangeException("frameCount", "Cannot wait for less that 1 frame");
            }*/

            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }
        }

    }
}
