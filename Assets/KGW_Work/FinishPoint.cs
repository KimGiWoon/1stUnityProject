using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneChange("ClearScene");
        }
        // 타이머 시작
        // 도착과 동시에 타이머 종료
        // 플레이 타임 저장
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.Inst.LoadScene(sceneName);
    }

    public void StartTimer()
    {

    }

    public void StopTimer()
    {

    }
}
