using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerprefsManager : MonoBehaviour {
    #region singleton

    private static PlayerprefsManager instance;
    public static PlayerprefsManager Inst {
        get {
            if (instance == null) {
                instance = FindObjectOfType<PlayerprefsManager>();

                if (instance == null) {
                    GameObject obj = new GameObject(nameof(PlayerprefsManager));
                    instance = obj.AddComponent<PlayerprefsManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    #endregion // singleton



    #region static fields

    private static string playerStringKey = "PlayerName";
    private static string playerScoreKey = "PlayerScore";

    #endregion



    #region mono funcs

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    #endregion // mono funcs





    #region public funcs

    public void SaveDate() {
        PlayerPrefs.SetInt(playerScoreKey, 100);
        PlayerPrefs.SetString(playerStringKey, "Player1");
        PlayerPrefs.Save();
    }

    public void GetData(ref int playerScore, ref string playerName) {
        playerScore = PlayerPrefs.GetInt(playerScoreKey, 0);
        playerName = PlayerPrefs.GetString(playerStringKey, "DefaultName");
    }

    #endregion // public funcs
}