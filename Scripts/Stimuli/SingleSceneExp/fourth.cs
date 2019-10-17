using System.Collections;
using UnityEngine;

public class fourth : MonoBehaviour {

    Vector3[] scales = new[] { new Vector3(0.03f, 0.03f, 0.03f), new Vector3(0.04f, 0.04f, 0.04f) };

    Color[] colors = new[] { new Color(0.4f, 0.4f, 0.4f), new Color(0.8f, 0.8f, 0.8f) };

    Color[] IconColors = new[] { new Color(0f, 0f, 0f), new Color(1f, 1f, 1f) };

    int currentScale;

    public SpriteRenderer Return;
    Transform GOtransform;
    SpriteRenderer GOspriterenderer;


    void Start () {
        currentScale = 0;

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
        Return.color = IconColors[currentScale];

        StartCoroutine(revisedSwitching());
    }

    IEnumerator revisedSwitching()
    {
        yield return new WaitForSeconds(6f);

        while (true)
        {
            yield return StartCoroutine(WaitFor.Frames(5));
            GOtransform.localScale = scales[currentScale];
            GOspriterenderer.color = colors[currentScale];
            Return.color = IconColors[currentScale];

            currentScale += 1;
            currentScale %= 2;

            if (currentScale == 1 && first.tempTime - first.secCount>2.2f)
            {
                yield return new WaitForSecondsRealtime(6f);
            }
        }

    }

    //-------------------------무시-------------------------//
    IEnumerator Switching()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            yield return StartCoroutine(WaitFor.Frames(5));
            //gameObject.GetComponent<Transform>().localScale = scales[currentScale];
            // gameObject.GetComponent<SpriteRenderer>().color = colors[currentScale];
            GOtransform.localScale = scales[currentScale];
            GOspriterenderer.color = colors[currentScale];
            Return.color = IconColors[currentScale];

            currentScale += 1;
            currentScale %= 2;

            if (currentScale == 1 && Time.time % 8 < 0.5)
            {

                yield return new WaitForSecondsRealtime(6f);

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
