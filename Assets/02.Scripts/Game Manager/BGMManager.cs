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

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        SetVolume(savedVolume); 

        // 오디오 재생
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();            
        }
    }

    public void SetVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        audioMixer.SetFloat("BGM", volume);        
    }
}
