using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 랭킹 등록 씬 괄호 안에 넣기
            //SceneManager.Inst.LoadScene();
            Debug.Log("게임 클리어!");
        }
    }
}
