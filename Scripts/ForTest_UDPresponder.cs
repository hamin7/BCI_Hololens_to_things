using UnityEngine;
using UnityEngine.SceneManagement;

public class ForTest_UDPresponder : MonoBehaviour {

    /// <summary>
    /// PC로부터 오는 UDP값에 따라 어플 전체를 총괄해야하는 스크립트. 
    /// UDP Listener와 짝지어서 싱글톤역할 
    /// </summary>
    
    //ORIGNAL SAMPLE CODE - show UDP value in 3D text//
    public TextMesh TextFromUDP = null;

    public void ResponseToUDPPacket(string fromIP, string fromPort, byte[] data)
    {
        string dataString = System.Text.Encoding.UTF8.GetString(data);

        if (TextFromUDP != null)
        {
            TextFromUDP.text = dataString;

        }

    }
 
    public static string EyeBlink4Times;

    public static string[] MethodSelect; // QR or Eye or SSVEP // IntroScene

    public static string ApplianceSelect; // 기기선택. QR, Eye, SSVEP 중 어떤 방식을 선택했느냐에 따라 기기 가짓수가 다름

    public static string[] CommandSelect; // 네개의 SSVEP 자극 중 1,2,3번 자극은 기기별 명령어. 4번 자극은 기기선택단계로 돌아감.

    public static GameObject StimuliSet;

    public static bool isQRFirstStage;

    private bool isOn = false;


    //Awake 하면 싱글톤에서 처리되기 전이라, 새로운 씬의 ForTest_UDPresponder의 Awake가 isQRFirstStage값을 바꿔놓을 수 있음
    private void Start() 
    {
            isQRFirstStage = true;
    }

    //-----------------------------CUSTOM FUNCTION---------------------------------//

        //눈 네번 깜빡임시 시스템 껐다 끄기
    public void EyeBlink(string fromIP, string fromPort, byte[] data)
    {
        string dataString = System.Text.Encoding.UTF8.GetString(data);
        if (dataString == EyeBlink4Times)
        {
            isOn = !isOn;

            /* IntroScene 이 첫 단계일때의 버전
            if (isOn) // off -> on 됐을때. off 되어있으면 현재 인트로 씬일수 밖에 없음
            {
                if(SceneManager.GetActiveScene().buildIndex != 0) //IntroScene
                {
                    return;
                }
                //Method Selection(디폴트로 씬 시작시 비활성화되어있어야함)을 위한 SSVEP 활성화
                StimuliSet.SetActive(true);
               
            }
            else
            {
                SceneManager.LoadScene(0); //IntroScene
            }*/

            if (isOn) // Off->On
            {
                MarkerControl.SystemON();
            }
            else //On -> Off
            {
                isQRFirstStage = true; //QR 씬에 원래 있는 GameObject와 싱글톤 충돌되어, 싱글톤 버려지고 초기값을 가진 정적변수가 들어가는듯
                SceneManager.LoadScene("QR"); // QR씬 로드하는 순간 지금의 싱글톤 정적변수는 destroy 되는듯
                //MarkerControl.SystemOFF();
               // MarkerControl.isFirstStage = true;
            }
        
            
        }
    }
    

    //IntroScene에서 선택된 씬으로 넘어가기 
    public void MethodSelection(string fromIP, string fromPort, byte[] data)
    {
        if (SceneManager.GetActiveScene().name != "IntroScene")
            return;

        string dataString = System.Text.Encoding.UTF8.GetString(data);
        switch (dataString)
        {
            /*HM중요*/
            //case : "매트랩으로부터 받은 UDP값" 
            
            case "QR": 
                //ON되어있는 QR씬으로 넘어가야함 
                isQRFirstStage = false;
                SceneManager.LoadScene("QR");
                //MarkerControl.SystemON(); //ERROR: 안먹힘
                break;
            case "SSVEP": //MethodSelect[0] 하면 '상수값이 필요합니다' 에러 메시지 뜸.
                SceneManager.LoadScene("SSVEP");
                break;
            case "100"://"Eye"
                SceneManager.LoadScene("Eye");
                //EyeWritingControl 에서 StimuliSet 정적변수에 해당 씬의 CommandStimuli 할당 후 비활성화시킴
                break;
            case "TurnSignal":
                //SceneManager.LoadScene(0)  OFF되어있는 QR씬으로 넘어가야함 
                isQRFirstStage = true;
                isOn = false;
                SceneManager.LoadScene("QR");

                break;

            default:
                break;
        }
    }

    //ReturnToApplianceSelect  (명령어선택단계에서 기기선택단계로 돌아가기 위함 ) 
    //public void ProcessManage(string fromIP, string fromPort, byte[] data)
    public void ReturnToApplianceSelect(string fromIP, string fromPort, byte[] data)
    {
        string dataString = System.Text.Encoding.UTF8.GetString(data);

        //if (dataString == '9'.ToString())
        /*if (dataString == EyeBlink4Times)//"MiddleWare#HanYang#TurnSignal"
        {
            MarkerControl.isSystemOn = !MarkerControl.isSystemOn;

            if (MarkerControl.isSystemOn == true)
            {
                MarkerControl.SystemON();
            } else if (MarkerControl.isSystemOn == false)
            {
                MarkerControl.SystemOFF();
            }
        }*/

        //if (dataString == '4'.ToString())
        /*HM 중요- 돌아가기 UDP값 미들웨어에서 추가되면   ||(or) 더해주기  */
        if (dataString == "MiddleWare#HanYang#RVC#Home" || dataString == "MiddleWare#HanYang#AirCleaner#Home" || dataString == "MiddleWare#HanYang#AirConditioner#Home" || dataString == "MiddleWare#HanYang#Bulb#Home")
        {
            //기기선택단계로 돌아가도록 씬 다시 로드
            isQRFirstStage = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    //QR 씬
    public void QRMethod(string fromIP, string fromPort, byte[] data)
    {
        if (SceneManager.GetActiveScene().name != "QR" )
            return;

        string dataString = System.Text.Encoding.UTF8.GetString(data);

        if (dataString =="90" ) //"BlinkChecker" 5초 내로 깜빡였는지 판별하는 값
        {
            SceneManager.LoadScene("IntroScene");
            //MarkerControl.isFirstStage = false;
        }else if(dataString == "80"){ //눈 8번 깜빡임값
            SceneManager.LoadScene("SSVEP");
        }else if(dataString == "120") //눈 12번 깜빡임값
        {
            SceneManager.LoadScene("Eye");
        }
    }


    //Eye Writing 씬에서의 기기선택
    public void EyeWritingMethod(string fromIP, string fromPort, byte[] data)
    {
        if (SceneManager.GetActiveScene().name != "Eye")
            return;

        string dataString = System.Text.Encoding.UTF8.GetString(data);

        ApplianceSelect = dataString; //Star_2 스크립트로 참조해서 어떤 아이콘 선택할지 판별

        var Spots = GameObject.Find("EyeWriting").transform.Find("Spots").gameObject;

        /*HM 중요 -  selectDevice UDP 값 더해지면 case : "...."  이하 똑같이 */
            switch (dataString)
            {
                case "0":
                    EyeWritingControl.PatternIcon.SetActive(true);
                    break;

                case "MiddleWare#HanYang#SelectDevice#RVC":
                    Spots.SetActive(false);
                    StimuliSet.SetActive(true);
                    break;

                case "MiddleWare#HanYang#SelectDevice#AirCleaner#01":
                    Spots.SetActive(false);
                    StimuliSet.SetActive(true);
                break;

                case "MiddleWare#HanYang#SelectDevice#AirCleaner#02":
                    Spots.SetActive(false);
                    StimuliSet.SetActive(true);
                break;

                case "MiddleWare#HanYang#SelectDevice#AirConditioner":
                    Spots.SetActive(false);
                    StimuliSet.SetActive(true);
                break;

                case "MiddleWare#HanYang#SelectDevice#Bulb":
                    Spots.SetActive(false);
                    StimuliSet.SetActive(true);
                break;

                case "6":
                    break;

                case "7":
                    break;

                case "8":
                    break;

                case "9":
                    break;

                default:
                    break;
            }
        
    }

    //SSVEP 기기선택
    public void SSVEPMethod(string fromIP, string fromPort, byte[] data)
    {
        if (SceneManager.GetActiveScene().name != "SSVEP")
            return;

            string dataString = System.Text.Encoding.UTF8.GetString(data);

            ApplianceSelect = dataString;

            var ApplianceStimuli = GameObject.Find("ApplianceStimuli");
        /*HM 중요 -  selectDevice UDP 값 더해지면 case : "...."  이하 똑같이 */
        switch (dataString)
            {
                case "MiddleWare#HanYang#SelectDevice#RVC":
                    StimuliSet.transform.position = ApplianceStimuli.transform.position;
                    ApplianceStimuli.SetActive(false);
                    StimuliSet.SetActive(true);
                    break;

                case "MiddleWare#HanYang#SelectDevice#AirCleaner#01":
                StimuliSet.transform.position = ApplianceStimuli.transform.position;
                ApplianceStimuli.SetActive(false);
                StimuliSet.SetActive(true);
                break;

                case "MiddleWare#HanYang#SelectDevice#AirCleaner#02":
                StimuliSet.transform.position = ApplianceStimuli.transform.position;
                ApplianceStimuli.SetActive(false);
                StimuliSet.SetActive(true);
                break;

            case "MiddleWare#HanYang#SelectDevice#AirConditioner":
                StimuliSet.transform.position = ApplianceStimuli.transform.position;
                ApplianceStimuli.SetActive(false);
                StimuliSet.SetActive(true);
                break;

            case "MiddleWare#HanYang#SelectDevice#Bulb":
                StimuliSet.transform.position = ApplianceStimuli.transform.position;
                ApplianceStimuli.SetActive(false);
                StimuliSet.SetActive(true);
                break;

            default:
                    break;

            }

    }
}
