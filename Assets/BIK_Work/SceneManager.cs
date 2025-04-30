using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    #region singleton

    private static SceneManager instance;
    public static SceneManager Inst {
        get {
            if (instance == null) {
                instance = FindObjectOfType<SceneManager>();

                if (instance == null) {
                    GameObject obj = new GameObject(nameof(SceneManager));
                    instance = obj.AddComponent<SceneManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    #endregion // singleton





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

    public void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    #endregion // public funcs
}