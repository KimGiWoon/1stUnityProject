using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;	// audioMixer를 사용하기 위해
using UnityEngine.UI;	// Slider, Toggle을 사용하기 위해

public class AudioMixController : MonoBehaviour
{  
    
        public AudioSource musicSource;
        public Slider volumeSlider;

        void Start()
        {
            // 슬라이더 초기값 설정
            volumeSlider.value = musicSource.volume;

            // 슬라이더 값이 변경될 때마다 볼륨 조절
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        void SetVolume(float value)
        {
            musicSource.volume = value;
        }
}
