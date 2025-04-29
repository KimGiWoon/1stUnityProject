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
                    GameObject obj = new GameObject(nameof(SaveManager));
                    instance = obj.AddComponent<SaveManager>();
                    DontDestroyOnLoad(obj); 
                }
            }
            return instance;
        }
    }

    #endregion

    #region Static Fields

    private static string playerStringKey = "PlayerName";   // Player Name Key
    private static string playerScoreKey = "PlayerScore";   // Player Score Key
    private static string playTimeKey = "PlayTime";         // Play Time Key
    private float startTime;    // 플레이 시작 시간
    private float endTime;      // 플레이 완료 시간
    private float totalPlayTime;    // 총 플레이 시간

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

    #region Start Timer On

    // 시작과 동시에 타이머 Start
    private void Start()
    {
        StartPlayTimer();
    }

    #endregion  // Start Timer On

    #region Public Funcs

    public void SaveDate()
    {
        PlayerPrefs.SetInt(playerScoreKey, 100);
        PlayerPrefs.SetString(playerStringKey, "Player1");
        PlayerPrefs.Save();
    }

    public void GetData(ref int playerScore, ref string playerName)
    {
        playerScore = PlayerPrefs.GetInt(playerScoreKey, 0);
        playerName = PlayerPrefs.GetString(playerStringKey, "DefaultName");
    }

    // 플레이 타임 저장
    public void SavePlayTime()
    {
        endTime = Time.time;
        totalPlayTime = endTime - startTime;
        PlayerPrefs.SetFloat(playTimeKey, totalPlayTime);   // 플레이 타임 키에 총 플레이 시간 저장
        PlayerPrefs.Save();
    }

    public float GetPlayTime()
    {
        return PlayerPrefs.GetFloat(playTimeKey, 0);
    }

    #endregion // Public Funcs

    #region Private Funcs

    // Start Timer Method
    private void StartPlayTimer()
    {
        startTime = Time.time;
    }

    #endregion Private Funcs
}
