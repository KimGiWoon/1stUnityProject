using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public AudioSource audioSource;
    public AudioMixer audioMixer;

    void Awake()
    {
        if (Instance == null)
        {
            
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환해도 유지            
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }
} // 주석
