using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boundingbox : MonoBehaviour , IFocusable {

   public Material BoundingBoxMat;
    Material CurrentMat;

    public Color ColorUnfocused;// = new Color(98f,132f,131f,190f);

    bool countStart = false;
    int framecount = 0;

    void OnEnable()
    {

        framecount = 0;
        countStart = false;
        SpriteRenderer sprenderer = GetComponent<SpriteRenderer>();
        //sprenderer.color = ColorUnfocused; -> objectFocused() 호출되어 ColorUnfocused 값 할당받기도전이라 문제생김 
    }

    public void OnFocusEnter()
    {
        //throw new System.NotImplementedException();
        objectFocused();
        countStart = true;
    }

    public void OnFocusExit()
    {
        // throw new System.NotImplementedException();
        objectUnfocused();
        framecount = 0;
        countStart = false;
    }

    void Update()
    {
        
        //자극물을 60 * n 프레임(n초) 이상 쳐다봤을때 select 처리
        if (countStart)
        {
        framecount++;
        if (framecount == 120) //2초 응시
            {
                
                MarkerControl.onSelect = true; //UDPGeneration 의 Update()내에서 캐치하여 UDP값 전송.

                /* HM 중요 */
                MarkerControl.SelectedMarker = gameObject.transform.root.name;


                Debug.Log("onSelect : Choose " + MarkerControl.SelectedMarker); // 이 시점에 UDP로 트리거 값 보내줘야함
                MarkerControl.ActivateMarker(false);
                MarkerControl.ActivateStimuli(true);
                MarkerControl.GazeCursor.SetActive(false); // 명령어 선택단계에서 거슬리는 게이즈 비활성화

                //onSelect하고 다시 명령어 선택단계로 돌아왔을때 이전에 인식된 아이콘 남아있지 않도록 처리해주어야함

                //다음번 QR 인식시 Icon의 색깔 초기로 재설정
                SpriteRenderer sprenderer = GetComponent<SpriteRenderer>();
                sprenderer.color = ColorUnfocused;

                framecount++; //framecount 반복문 벗어나도록. 그래야 위 명령어들 한번만 실행됨

            }
            
        }
    
    }

 
    void objectFocused()
    {
        //3D오브젝트 매터리얼의 경우, 특정 색상 입혀줄 매터리얼 레퍼런스
        /*MeshRenderer rend = GetComponent<MeshRenderer>();
        CurrentMat = rend.material;
        rend.material = BoundingBoxMat;*/


        //2D스프라이트의 경우, SpriteRendere 레퍼런스하여 투명회색조에서 새하얀흰색으로 바꿈

        SpriteRenderer sprenderer = GetComponent<SpriteRenderer>();
        ColorUnfocused = sprenderer.color;
        sprenderer.color = new Color(255f,255f,255f, 255f);

        //sprenderer.material = BoundingBoxMat;
        //Debug.Log("color onFocused: " + sprenderer.color); -> new color(50,50,50,100)했을 경우 이 값 그대로 출력되지만 플레이모드 화면상 색변화 없음. 0값줘서 검은색으로 바꾸는건 가능. 
    }

    void objectUnfocused()
    {
        //3D오브젝트 매터리얼
        /*MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.material = CurrentMat;*/

        //2D스프라이트
        SpriteRenderer sprenderer = GetComponent<SpriteRenderer>();
        sprenderer.color = ColorUnfocused;
    }
}
