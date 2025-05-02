using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SoundOptions : MonoBehaviour // 수정
{
    // 오디오 믹서
    public AudioMixer audioMixer;

    // 슬라이더
    public Slider BgmSlider;

    // 볼륨 조절
    public void SetBgmVolme()
    {

        // 로그 연산 값 전달
        audioMixer.SetFloat("BGM", Mathf.Log10(BgmSlider.value) * 20);
    }

}