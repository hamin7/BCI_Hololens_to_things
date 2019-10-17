using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_1 : MonoBehaviour {

    public SSVEPsetting setting;
    public static Vector3[] Scales;
    public static Color[] Colors;
    public static Color[] IconColors;


    public static int checker_1, checker_2;
    public static float secCount;
    public static float tempTime;

    public static float TrialDuration;
    public static float TimeBetweenTrial;

    int currentScale;
    Transform GOtransform;
    SpriteRenderer GOspriterenderer;
    SpriteRenderer Icon;

    public TextMesh CenterOfStimuli;
    public int FrameCount;

    void Start () {
        currentScale = 0;
        secCount = 0;

        GOtransform = gameObject.GetComponent<Transform>();
        GOspriterenderer = gameObject.GetComponent<SpriteRenderer>();
        Icon = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        Icon.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(revisedSwitching());
    }
    private void OnEnable()
    {

        secCount = 0;
        currentScale = 0;
        GOtransform.localScale = Scales[currentScale];
        GOspriterenderer.color = Colors[currentScale];
        Icon.color = IconColors[currentScale];

        StartCoroutine(revisedSwitching());
    }
    IEnumerator CountDown()
    {
        //첫 트라이얼만 3초 쉬고 시작. secCount = 0 일때 스킵
        //secCount 0 넘고나서 부터는 앞에도 3초쉬고 뒤에서도 3초 쉬기
        //CommandStimuli만 해당되는 명령어
        if (gameObject.transform.root.name == "CommandStimuli")
        {
            if (secCount != 0)
            {
                yield return new WaitForSeconds(3f);
            }
        }

        CenterOfStimuli.fontSize = 170;
        CenterOfStimuli.text = "3";
        yield return new WaitForSeconds(1f);

        CenterOfStimuli.text = "2";
        yield return new WaitForSeconds(1f);

        CenterOfStimuli.text = "1";
        yield return new WaitForSeconds(1f);

        CenterOfStimuli.text = "";

    }
    IEnumerator revisedSwitching()
    {
        CenterOfStimuli.fontSize = 48;
        CenterOfStimuli.text = "잠시후\n시작됩니다";
        StartCoroutine(CountDown());

        //첫 트라이얼만 3초 더 쉬고 시작
        if (secCount == 0)
        {
                yield return new WaitForSeconds(3f);
        }
        else
        {
            yield return new WaitForSeconds(TimeBetweenTrial);
        }

        secCount = Time.time;

        //STARTING POINT    
        checker_1 = 0;
        checker_2 = 1;

        yield return new WaitForSeconds(0.01f); //0.01초 delay해주어야만 Update()의 한 프레임에서 캐치함


        checker_1 = 1;
        checker_2 = 1;

        while (true)
        {

            yield return StartCoroutine(WaitFor.Frames(FrameCount));

            GOtransform.localScale = Scales[currentScale];
            GOspriterenderer.color = Colors[currentScale];
            Icon.color = IconColors[currentScale];

            currentScale += 1;
            currentScale %= 2;

            tempTime = Time.time;

            if (currentScale == 1 && tempTime - secCount > TrialDuration )
            {
                //ENDING POINT
                checker_1 = 1;
                checker_2 = 0;

                yield return new WaitForSeconds(0.01f);


                // trial 사이의 pause
                checker_1 = 0;
                checker_2 = 0;

                StartCoroutine(CountDown());
                yield return new WaitForSecondsRealtime(TimeBetweenTrial);

                //pause 후 시작시점에 secCount 새값 할당
                secCount = Time.time;

                //STARTING POINT    
                checker_1 = 0;
                checker_2 = 1;

                yield return new WaitForSeconds(0.01f); //0.01초 delay해주어야만 Update()의 한 프레임에서 캐치함\


                checker_1 = 1;
                checker_2 = 1;
            }
        }
    }

    /*FRAME COUNT*/
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
