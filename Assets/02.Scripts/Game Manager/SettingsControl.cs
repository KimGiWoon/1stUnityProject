using System.Collections; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsControl : MonoBehaviour
{
    [Header("슬라이더")]
    public Slider brightnessSlider;
    public Slider sensitivitySlider;
    public Slider bgmSlider;

    [Header("밝기 오버레이")]
    public Image brightnessOverlay;

    [Header("오디오")]
    public AudioMixer audioMixer;

    [Header("패널")]
    public GameObject optionPanel;

    private float initialBrightness;
    private float initialSensitivity;
    private float initialBGM;

    IEnumerator Start()
    {
        yield return null; 

        // 저장된 값 불러오기
        initialBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        initialSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        initialBGM = PlayerPrefs.GetFloat("BGMVolume", 1f);        

        // 슬라이더 값 UI에 적용 
        brightnessSlider.SetValueWithoutNotify(initialBrightness);
        sensitivitySlider.SetValueWithoutNotify(initialSensitivity);
        bgmSlider.SetValueWithoutNotify(initialBGM);

        // 실제 게임 시스템에 값 반영
        ApplyBrightness(initialBrightness);
        SetBGMVolume(initialBGM); // BGM 소리 적용

        // 슬라이더 값 변경 시 이벤트 연결
        brightnessSlider.onValueChanged.AddListener(ApplyBrightness);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    void ApplyBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            Color color = brightnessOverlay.color;
            color.a = 1f - value;
            brightnessOverlay.color = color;
        }
    }

    void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value); // 즉시 저장
    }

    void SetBGMVolume(float value)
    {
        PlayerPrefs.SetFloat("BGMVolume", value); // 즉시 저장
        BGMManager.Instance?.SetVolume(value);
    }

    public void OnClickOK()
    {
        float currentBrightness = brightnessSlider.value;
        float currentSensitivity = sensitivitySlider.value;
        float currentBGM = bgmSlider.value;

        // 값 저장
        PlayerPrefs.SetFloat("Brightness", currentBrightness);
        PlayerPrefs.SetFloat("MouseSensitivity", currentSensitivity);
        PlayerPrefs.SetFloat("BGMVolume", currentBGM);
        PlayerPrefs.Save();

        // 반영
        ApplyBrightness(currentBrightness);
        BGMManager.Instance?.SetVolume(currentBGM);

        // 초기값 갱신
        initialBrightness = currentBrightness;
        initialSensitivity = currentSensitivity;
        initialBGM = currentBGM;

        optionPanel.SetActive(false);
    }

    public void OnClickCancel()
    {
        brightnessSlider.SetValueWithoutNotify(initialBrightness);
        sensitivitySlider.SetValueWithoutNotify(initialSensitivity);
        bgmSlider.SetValueWithoutNotify(initialBGM);

        ApplyBrightness(initialBrightness);
        BGMManager.Instance?.SetVolume(initialBGM);

        optionPanel.SetActive(false);
    }

    public void OpenOptionPanel()
    {
        optionPanel.SetActive(true);
    }
}
