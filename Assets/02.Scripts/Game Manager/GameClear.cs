using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    float currentTime;  // Play Time
    float maxTime = 60f;
    int minuteTime = 0;
    
    bool ClearGame;

    private void Awake()
    {
        ClearGame = false;
    }

    private void Update()
    {
        if (!ClearGame)
        {
            CurrentTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ClearGame = true;
            GameManager.Inst.OnPlayerReachedGoal();
        }
    }

    // 플레이 시간
    public void CurrentTimer()
    {
        currentTime += Time.deltaTime;

        SaveManager.Inst.SaveSecondPlayTime(currentTime % 60);     // SaveManager에 저장

        if (currentTime >= maxTime)
        {
            minuteTime += 1;
            currentTime -= maxTime;
        }
        SaveManager.Inst.SaveMinutePlayTime(minuteTime);     
    }
}
