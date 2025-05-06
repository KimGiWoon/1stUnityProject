using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    float startTime;
    float stopTime;

    private void Start()
    {
        StartTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StopTimer();

            float playTime = TotalPlayTime(startTime, stopTime);
            SaveManager.Inst.SaveSecondPlayTime(playTime);  // SaveManager에 저장

            SceneChange("ClearScene");

        }
    }

    // 씬 체인지
    public void SceneChange(string sceneName)
    {
        SceneManager.Inst.LoadScene(sceneName);
    }

    // 플레이 시작 시간
    public void StartTimer()
    {
        startTime = Time.time;
    }

    // 클리어 시간
    public void StopTimer()
    {
        stopTime = Time.time;
    }

    // 총 플레이 시간
    public float TotalPlayTime(float startTime, float stopTime)
    {
        return stopTime - startTime;
    }
}
