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

    void Start()
    {
        // 초기값 불러오기
        initialBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        initialSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        initialBGM = PlayerPrefs.GetFloat("BGMVolume", 1f);

        // 슬라이더에 값 적용
        brightnessSlider.SetValueWithoutNotify(initialBrightness);
        sensitivitySlider.SetValueWithoutNotify(initialSensitivity);
        bgmSlider.SetValueWithoutNotify(initialBGM);

        // 반영
        ApplyBrightness(initialBrightness);
        BGMManager.Instance?.SetVolume(initialBGM);

        // 슬라이더 이벤트 연결
        brightnessSlider.onValueChanged.AddListener(ApplyBrightness);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);

        float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 1f);
        if (savedBGM <= 0f)
        {
            savedBGM = 1f;
            PlayerPrefs.SetFloat("BGMVolume", savedBGM);
            PlayerPrefs.Save();
        }
        initialBGM = savedBGM;
        bgmSlider.SetValueWithoutNotify(initialBGM);
        BGMManager.Instance?.SetVolume(initialBGM);
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
        PlayerPrefs.SetFloat("MouseSensitivity", value);
    }

    void SetBGMVolume(float value)
    {
        PlayerPrefs.SetFloat("BGMVolume", value);
        BGMManager.Instance?.SetVolume(value);
    }

    public void OnClickOK()
    {
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivitySlider.value);
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.Save();
        ApplyBrightness(brightnessSlider.value);

        optionPanel.SetActive(false);
    }

    public void OnClickCancel()
    {
        brightnessSlider.SetValueWithoutNotify(initialBrightness);
        sensitivitySlider.SetValueWithoutNotify(initialSensitivity);
        bgmSlider.SetValueWithoutNotify(initialBGM);

        ApplyBrightness(initialBrightness);
        BGMManager.Instance?.SetVolume(initialBGM);

        optionPanel.SetActive(false); // 옵션창 닫기
    }

    public void OpenOptionPanel()
    {
        optionPanel.SetActive(true);
    }
}