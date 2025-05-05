using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    float currentTime;  // Play Time
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
            //랭킹 등록 씬 괄호 안에 넣기
            SceneManager.Inst.LoadClearScene();
            //Debug.Log("게임 클리어!");
        }
    }

    // 플레이 시간
    public void CurrentTimer()
    {
        currentTime += Time.deltaTime;
        SaveManager.Inst.SavePlayTime(currentTime);     // SaveManager에 저장
    }
}
