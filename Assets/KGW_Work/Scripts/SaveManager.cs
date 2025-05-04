using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton

    private static SaveManager instance;
    public static SaveManager Inst  // Save Manager 생성
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

    const int rankListMaxCount = 25;    // 랭킹 순위 세팅

    #region Static Fields

    private static string playerStringKey = "PlayerName";   // Player Name Key
    private static string playTimeKey = "PlayTime";         // Play Time Key
    //private static string playerScoreKey = "PlayerScore";   // Player Score Key
    //private static string lastPlayDateKey = "LastPlayDate";         // Play Time Key

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

    public string GetData()
    {
        //playerScore = PlayerPrefs.GetInt(playerScoreKey, 0);  // 플레이어 점수 미구현
        string playerName = PlayerPrefs.GetString(playerStringKey, "DefaultName");
        return playerName;
    }

    // 플레이 타임 저장
    public void SavePlayTime(float totalPlayTime)
    {
        PlayerPrefs.SetFloat(playTimeKey, totalPlayTime);   // 플레이 타임 키에 총 플레이 시간 저장
        PlayerPrefs.Save();
    }

    public float GetPlayTime()
    {
        return PlayerPrefs.GetFloat(playTimeKey, 0);    // 플레이 시간이 없으면 0 반환
    }

    public void SaveRankingData(string playerName, float playTime)
    {
        List<(string name, float time)> rankList = new List<(string name, float time)>();

        for (int i = 0; i < rankListMaxCount; i++)
        {
            string name = PlayerPrefs.GetString($"PlayerName_{i}", "");
            float time = PlayerPrefs.GetFloat($"PlayTime_{i}", -1f);

            if (!string.IsNullOrEmpty(name) && time >= 0)
            {
                rankList.Add((name, time));
            }
        }

        // 새 랭킹 추가
        rankList.Add((playerName, playTime));

        // 정렬 및 상위 10개 유지
        rankList = rankList.OrderBy(x => x.time).Take(rankListMaxCount).ToList();

        for (int i = 0; i < rankListMaxCount; i++)
        {
            if (i < rankList.Count)
            {
                PlayerPrefs.SetString($"PlayerName_{i}", rankList[i].name);
                PlayerPrefs.SetFloat($"PlayTime_{i}", rankList[i].time);
            }
            else
            {
                PlayerPrefs.DeleteKey($"PlayerName_{i}");
                PlayerPrefs.DeleteKey($"PlayTime_{i}");
            }
        }

        PlayerPrefs.Save();
    }

    // 저장된 랭크 리스트 가져오기
    public List<(string name, float time)> GetRankingData()
    {
        List<(string name, float time)> rankList = new List<(string name, float time)>();

        for (int i = 0; i < rankListMaxCount; i++)
        {
            string name = PlayerPrefs.GetString($"PlayerName_{i}", "");
            float time = PlayerPrefs.GetFloat($"PlayTime_{i}", -1f);

            if (!string.IsNullOrEmpty(name) && time >= 0)
            {
                rankList.Add((name, time));
            }
            else
            {
                name = "Not ranking data.";
                time = 0;
                rankList.Add((name, time));
            }
        }

        return rankList;
    }

    // 랭킹 리스트 초기화 
    public void ClearRankingData()
    {
        // 리스트를 순회 하면서 키에 대한 내용 삭제
        for (int i = 0; i < rankListMaxCount; i++)
        {
            PlayerPrefs.DeleteKey($"PlayerName_{i}");
            PlayerPrefs.DeleteKey($"PlayTime_{i}");
        }

        // 정상 삭제 확인
        PlayerPrefs.Save();
        Debug.Log("랭킹 데이터 초기화 완료");
    }
    #endregion // Public Funcs
}
