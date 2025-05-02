using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour // 수정
{
    [Header("슬라이더들")]
    public Slider brightnessSlider;
    public Slider bgmSlider;
    public Slider sensitivitySlider;

    [Header("오버레이")]
    public Image brightnessOverlay;

    // 초기값 저장용
    private float initialBrightness;
    private float initialBGM;
    private float initialSensitivity;

    void Start()
    {
        // 값 불러오기 (없으면 기본값)
        float brightness = PlayerPrefs.GetFloat("Brightness", 1f);
        Debug.Log("불러온 밝기 값: " + brightness);
        float bgm = PlayerPrefs.GetFloat("BGM", 1f);
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);

        // 슬라이더 초기화
        brightnessSlider.SetValueWithoutNotify(brightness);
        bgmSlider.SetValueWithoutNotify(bgm);
        sensitivitySlider.SetValueWithoutNotify(sensitivity);

        // UI 반영
        SetBrightness(brightness);
        SetBGMVolume(bgm);
        SetSensitivity(sensitivity);

        // 초기값 저장
        initialBrightness = brightness;
        initialBGM = bgm;
        initialSensitivity = sensitivity;

        // 이벤트 연결
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }

    // 슬라이더별 처리 함수들
    public void SetBrightness(float value)
    {
        Color color = brightnessOverlay.color;
        color.a = 1f - value;
        brightnessOverlay.color = color;
    }

    public void SetBGMVolume(float value)
    {
        // 여기에 AudioMixer 연동이 있다면 추가 (예: audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20); )
        //Debug.Log($"BGM Volume: {value}");
    }

    public void SetSensitivity(float value)
    {
        // 민감도 처리 (예: MouseSensitivityManager.sensitivity = value;)
        //  Debug.Log($"Mouse Sensitivity: {value}");
    }

    //  OK 버튼
    public void OnClickOK()
    {
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        PlayerPrefs.Save();

        initialBrightness = brightnessSlider.value;
        initialBGM = bgmSlider.value;
        initialSensitivity = sensitivitySlider.value;
    }

    //  Cancel 버튼
    public void OnClickCancel()
    {
        brightnessSlider.SetValueWithoutNotify(initialBrightness);
        bgmSlider.SetValueWithoutNotify(initialBGM);
        sensitivitySlider.SetValueWithoutNotify(initialSensitivity);

        SetBrightness(initialBrightness);
        SetBGMVolume(initialBGM);
        SetSensitivity(initialSensitivity);
    }
}
