using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSVEPsetting : MonoBehaviour {

    //public int TrialCount;
    public float TrialDuration;
    public float TimeBetweenTrial;

    public Vector3[] Scales;
    public Color[] Colors;
    public Color[] CommandIconColors;

    private void Awake()
    {
        Star_1.Scales = Scales;
        Star_1.Colors = Colors;
        Star_1.IconColors = CommandIconColors;

        Star_1.TrialDuration = TrialDuration - 0.3f; // 실제 TrialDuration 보다 0.3초정도 더 걸려서 미리 빼줌.
        Star_1.TimeBetweenTrial = TimeBetweenTrial;
    }

}
