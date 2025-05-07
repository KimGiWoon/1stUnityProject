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

    #region private Fields

    const int rankListMaxCount = 25;    // 랭킹 순위 세팅

    #endregion

    #region Static Fields

    private static string playerStringKey = "PlayerName";   // Player Name Key
    private static string playTimeKey = "PlayTime";         // Play Time Key
    private static string playMinuteTimeKey = "PlayMinuteTime";         // Play Minute Time Key
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
    public void SaveSecondPlayTime(float secondPlayTime)
    {
        PlayerPrefs.SetFloat(playTimeKey, secondPlayTime);   // 플레이 타임 키에 총 플레이 시간 저장
        PlayerPrefs.Save();
    }

    public float GetSecondPlayTime()
    {
        return PlayerPrefs.GetFloat(playTimeKey, 0);    // 플레이 시간이 없으면 0 반환
    }

    // 플레이 타임(분) 저장
    public void SaveMinutePlayTime(int minutePlayTime)
    {
        PlayerPrefs.SetInt(playMinuteTimeKey, minutePlayTime);   // 플레이 타임(분) 키에 총 시간{분) 저장
        PlayerPrefs.Save();
    }

    public int GetMinutePlayTime()
    {
        return PlayerPrefs.GetInt(playMinuteTimeKey, 0);    // 플레이 시간(분)이 없으면 0 반환
    }

    public void SaveRankingData(string playerName, float secondTime, int minuteTime)
    {
        List<(string name, float secondTime, int minuteTime)> rankList = new List<(string name, float secondTime, int minuteTime)>();

        for (int i = 0; i < rankListMaxCount; i++)
        {
            string name = PlayerPrefs.GetString($"PlayerName_{i}", "");
            float sTime = PlayerPrefs.GetFloat($"PlayTime_{i}", -1f);
            int mTime = PlayerPrefs.GetInt($"PlayMinuteTime_{i}", -1);

            if (!string.IsNullOrEmpty(name) && sTime >= 0 && mTime >= 0)
            {
                rankList.Add((name, sTime, mTime));
            }
        }

        // 새 랭킹 추가
        rankList.Add((playerName, secondTime, minuteTime));

        // 정렬 및 상위 10개 유지
        rankList = rankList.OrderBy(x => x.minuteTime).ThenBy(x => x.secondTime).Take(rankListMaxCount).ToList();

        for (int i = 0; i < rankListMaxCount; i++)
        {
            if (i < rankList.Count)
            {
                PlayerPrefs.SetString($"PlayerName_{i}", rankList[i].name);
                PlayerPrefs.SetFloat($"PlayTime_{i}", rankList[i].secondTime);
                PlayerPrefs.SetInt($"PlayMinuteTime_{i}", rankList[i].minuteTime);
            }
            else
            {
                PlayerPrefs.DeleteKey($"PlayerName_{i}");
                PlayerPrefs.DeleteKey($"PlayTime_{i}");
                PlayerPrefs.DeleteKey($"PlayMinuteTime_{i}");
            }
        }

        PlayerPrefs.Save();
    }

    // 저장된 랭크 리스트 가져오기
    public List<(string name, float secondTime, int minuteTime)> GetRankingData()
    {
        List<(string name, float secondTime, int minuteTime)> rankList = new List<(string name, float secondTime, int minuteTime)>();

        for (int i = 0; i < rankListMaxCount; i++)
        {
            string name = PlayerPrefs.GetString($"PlayerName_{i}", "");
            float sTime = PlayerPrefs.GetFloat($"PlayTime_{i}", -1f);
            int mTime = PlayerPrefs.GetInt($"PlayMinuteTime_{i}", -1);

            if (!string.IsNullOrEmpty(name) && sTime >= 0 && mTime >= 0)
            {
                rankList.Add((name, sTime, mTime));
            }
            else
            {
                name = "Not ranking data.";
                sTime = 0;
                mTime = 0;
                rankList.Add((name, sTime, mTime));
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
            PlayerPrefs.DeleteKey($"PlayMinuteTime_{i}");
        }

        // 정상 삭제 확인
        PlayerPrefs.Save();
        //Debug.Log("랭킹 데이터 초기화 완료");
    }
    #endregion // Public Funcs
}
