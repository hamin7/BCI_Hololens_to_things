using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first : MonoBehaviour {

    Vector3[] scales = new[] { new Vector3(0.03f, 0.03f, 0.03f), new Vector3(0.04f, 0.04f, 0.04f) };

    Color[] colors = new[] { new Color(0.4f, 0.4f, 0.4f), new Color(0.8f, 0.8f, 0.8f) };

    Color[] IconColors = new[] { new Color(0f, 0f, 0f), new Color(1f, 1f, 1f) };

    int currentScale;

    public string start, end;
    public static int checker_1, checker_2;
    public static float secCount;
    public static float tempTime;

    public SpriteRenderer OnOff;
    Transform GOtransform;
    SpriteRenderer GOspriterenderer;

    public TextMesh CenterOfStimuli;


    void Start () {
        currentScale = 0;
        //start = "01";
        //end = "10";
        start = "HololenseHY#Star#01#Start";
        end = "HololenseHY#Star#01#End";

        secCount = 0;

        GOtransform = gameObject.GetComponent<Transform>();
        GOspriterenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	

    private void OnEnable()
    {
        //StartCoroutine(Switching());

        //1.0.11.0 이후
        currentScale = 0;
        GOtransform.localScale = scales[currentScale];
        GOspriterenderer.color = colors[currentScale];
        OnOff.color = IconColors[currentScale];

        StartCoroutine(revisedSwitching());
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3f);
        CenterOfStimuli.fontSize=170;
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
        yield return new WaitForSeconds(6f);
        secCount = Time.time;

        //STARTING POINT    
        checker_1 = 0;
        checker_2 = 1;

        yield return new WaitForSeconds(0.01f); //0.01초 delay해주어야만 Update()의 한 프레임에서 캐치함
        

         checker_1 = 1;
        checker_2 = 1;

        while (true)
        {
            
            yield return StartCoroutine(WaitFor.Frames(8));

            GOtransform.localScale = scales[currentScale];
            GOspriterenderer.color = colors[currentScale];
            OnOff.color = IconColors[currentScale];

            currentScale += 1;
            currentScale %= 2;

            tempTime = Time.time;

            if(currentScale == 1 && tempTime - secCount > 2.2f)
            {
                //ENDING POINT
                checker_1 = 1;
                checker_2 = 0;

                 yield return new WaitForSeconds(0.01f);
                

                //5초간 pause
                checker_1 = 0;
                checker_2 = 0;

                StartCoroutine(CountDown());
                yield return new WaitForSecondsRealtime(6f);
                
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

    //-------------------------무시-------------------------//
    IEnumerator Switching()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            yield return StartCoroutine(WaitFor.Frames(8));
            //gameObject.GetComponent<Transform>().localScale = scales[currentScale];
            //gameObject.GetComponent<SpriteRenderer>().color = colors[currentScale];
            GOtransform.localScale = scales[currentScale];
            GOspriterenderer.color = colors[currentScale];
            OnOff.color = IconColors[currentScale];

            currentScale += 1;
            currentScale %= 2;

            if (currentScale == 1 && Time.time % 8 < 0.5) // Time.time%6 하고 WaitForSecondsRealtime(5f)로 바꿨더니 1초만 깜빡임
            {
                //ENDING POINT
                checker_1 = 1;
                checker_2 = 0;

                yield return new WaitForSeconds(0.01f);

                //3초간 pause
                checker_1 = 0;
                checker_2 = 0;


                yield return new WaitForSecondsRealtime(5f);

                //STARTING POINT    
                checker_1 = 0;
                checker_2 = 1;

                yield return new WaitForSeconds(0.01f); //0.01초 delay해주어야만 Update()의 한 프레임에서 캐치함


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
