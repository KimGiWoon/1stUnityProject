using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance;

    public Image brightnessOverlay; // Canvas 아래 Image
    public GameObject brightnessCanvas; // 전체 Canvas

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);           // BrightnessManager 유지
            DontDestroyOnLoad(brightnessCanvas);     // BrightnessCanvas도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        ApplyBrightness(savedBrightness);
    }

    public void ApplyBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            Color c = brightnessOverlay.color;
            c.a = 1f - value;
            brightnessOverlay.color = c;
        }
    }
}
