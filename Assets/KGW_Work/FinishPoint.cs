using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.Inst.LoadScene("ClearScene");
        }
        // 타이머 시작
        // 도착과 동시에 타이머 종료
        // 플레이 타임 저장
    }
}
