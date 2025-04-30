using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton

    private static SaveManager instance;
    public static SaveManager Instance  // Save Manager 생성
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("SaveManager");
                    instance = obj.AddComponent<SaveManager>();
                    DontDestroyOnLoad(obj); 
                }
            }
            return instance;
        }
    }

    #endregion

    const int rankListMaxCount = 30;

    #region Static Fields

    private static string playerStringKey = "PlayerName";   // Player Name Key
    //private static string playerScoreKey = "PlayerScore";   // Player Score Key
    private static string playTimeKey = "PlayTime";         // Play Time Key
    private static string lastPlayDateKey = "LastPlayDate";         // Play Time Key

    #endregion

    #region Mono Funcs

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;    // 처음 생성한 Manager Setting
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복 생성 시 삭제
        }
    }

    #endregion

    #region Public Funcs

    // 플레이어의 이름 저장
    public void SaveDate(string playerName)
    {
        //PlayerPrefs.SetInt(playerScoreKey, 100);  // 플레이어 점수 미구현
        PlayerPrefs.SetString(playerStringKey, playerName);
        PlayerPrefs.Save();
    }

    public void GetData(out string playerName)
    {
        //playerScore = PlayerPrefs.GetInt(playerScoreKey, 0);  // 플레이어 점수 미구현
        playerName = PlayerPrefs.GetString(playerStringKey, "DefaultName");
    }

    // 플레이 타임 저장
    public void SavePlayTime(ref float startTime, ref float endTime)
    {
        float totalPlayTime = endTime - startTime; // 총 플레이 시간
        PlayerPrefs.SetFloat(playTimeKey, totalPlayTime);   // 플레이 타임 키에 총 플레이 시간 저장
        PlayerPrefs.Save();
    }

    public float GetPlayTime()
    {
        return PlayerPrefs.GetFloat(playTimeKey, 0);    // 플레이 시간이 없으면 0 반환
    }

    // 마지막 플레이 날짜 저장
    public void SaveLastPlayDate()
    {
        string playdate = System.DateTime.Now.ToString("MM-dd");    // 마지막 플레이 월 - 일 저장
        PlayerPrefs.SetString(lastPlayDateKey, playdate);
        PlayerPrefs.Save();
    }

    public string GetLastPlayTime()
    {
        return PlayerPrefs.GetString(lastPlayDateKey, "NotExist");  // 마지막 플레이 데이터가 없으면 "존내하지 않는다"는 문구 출력
    }

    public void SaveRankingData()
    {
        float ClearPlayTime = GetPlayTime();    // 플레이 타임 가져오기

        List<(string name, float time)> rankList = new List<(string name, float time)>();

        for(int i =0; i < rankListMaxCount; i++)
        {
            //string name = PlayerPrefs.get
        }
    }

    #endregion // Public Funcs
}
