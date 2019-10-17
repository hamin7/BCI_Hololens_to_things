using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour {

    //실제 기기 바라보는 각도로 하면 게이즈 커서 충돌처리가 안됨. 그리고 충돌처리 되더라도 커서 너무 작음

    //1.0.1.0 버전 커서 빼고 아무것도 안돌아감. 텍스트도 안보임. MarkerControl 때문인지, 아니면 텍스트 위치가 문제인건지 잘 모르겠음
    //너무 무겁게 하는것 같아  MarkerControl  컴포넌트 삭제

    //1.0.2.0 버전: 3D 텍스트 중 FPS display 만 보임. 마커 인식 성공. 
    //전전버전에서 잘 보이던 true/false 텍스트가 안보임. 디폴트값 문구도 안보임. 카메라 child 처리해서 원인인듯.


    //1.0.3.0 버전: TextFromUDP 카메라 child에서 빼니까 보임. 9값에 따라 true/false도 바뀜. 
    // 그런데 onSelect하고 로드된 씬에서 자극 오브젝트 생성되기도 전에 다시 MarkerInteration 씬으로 돌아옴(로드되긴 함)
    //싱글톤 isOn 값이 Start() 안에서 매 씬마다 false로 초기화 되는것 같음(유니티 에디터에선 안그러는데..)
    //마커인식후 뜨는 자극과 커서크기, 거리가 안 맞음
    //다시 돌아온 MarkerInteraction 씬에서는 isOn값 텍스트가 안먹히고 기본 문구 "TextFromUDP"만 보임. 
    
     //싱글콘 객체 참조 문제, 싱글톤 Start() 문제 , 커서 문제, 로드된 씬 화면 위치조정문제

    public static Singleton instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {

        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }
 
    
}
