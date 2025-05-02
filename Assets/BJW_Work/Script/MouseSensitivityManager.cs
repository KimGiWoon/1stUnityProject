using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityManager : MonoBehaviour // 수정
{
    public Slider sensitivitySlider;
    public static float sensitivity = 1f;

    void Start()
    {
        // 슬라이더에 값 주지 않고, 있는 값에서 가져오기만 함
        sensitivity = sensitivitySlider.value;

        // 슬라이더가 변경될 때마다 감도 업데이트
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    public void UpdateSensitivity(float value)
    {
        sensitivity = value;

       
    }
}
