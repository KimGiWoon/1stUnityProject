using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton

    private static SaveManager instance;
    public static SaveManager Instance  // Save Manager ����
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
            instance = this;    // ó�� ������ Manager Setting
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ߺ� ���� �� ����
        }
    }

    #endregion

    #region Public Funcs

    // �÷��̾��� �̸� ����
    public void SaveDate(string playerName)
    {
        //PlayerPrefs.SetInt(playerScoreKey, 100);  // �÷��̾� ���� �̱���
        PlayerPrefs.SetString(playerStringKey, playerName);
        PlayerPrefs.Save();
    }

    public void GetData(out string playerName)
    {
        //playerScore = PlayerPrefs.GetInt(playerScoreKey, 0);  // �÷��̾� ���� �̱���
        playerName = PlayerPrefs.GetString(playerStringKey, "DefaultName");
    }

    // �÷��� Ÿ�� ����
    public void SavePlayTime(ref float startTime, ref float endTime)
    {
        float totalPlayTime = endTime - startTime; // �� �÷��� �ð�
        PlayerPrefs.SetFloat(playTimeKey, totalPlayTime);   // �÷��� Ÿ�� Ű�� �� �÷��� �ð� ����
        PlayerPrefs.Save();
    }

    public float GetPlayTime()
    {
        return PlayerPrefs.GetFloat(playTimeKey, 0);    // �÷��� �ð��� ������ 0 ��ȯ
    }

    // ������ �÷��� ��¥ ����
    public void SaveLastPlayDate()
    {
        string playdate = System.DateTime.Now.ToString("MM-dd");    // ������ �÷��� �� - �� ����
        PlayerPrefs.SetString(lastPlayDateKey, playdate);
        PlayerPrefs.Save();
    }

    public string GetLastPlayTime()
    {
        return PlayerPrefs.GetString(lastPlayDateKey, "NotExist");  // ������ �÷��� �����Ͱ� ������ "�������� �ʴ´�"�� ���� ���
    }

    public void SaveRankingData()
    {
        float ClearPlayTime = GetPlayTime();    // �÷��� Ÿ�� ��������

        List<(string name, float time)> rankList = new List<(string name, float time)>();

        for(int i =0; i < rankListMaxCount; i++)
        {
            //string name = PlayerPrefs.get
        }
    }

    #endregion // Public Funcs
}
