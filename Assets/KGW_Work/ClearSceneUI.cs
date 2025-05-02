using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearSceneUI : MonoBehaviour
{
    [SerializeField] TMP_InputField inputFieldUI;
    [SerializeField] Scrollbar ScrollbarUI;
    [SerializeField] Transform contentParent;   // 스크롤 뷰 오브젝트
    [SerializeField] GameObject rankingPrefab;

    // 스크롤바에 플레이어 이름 입력
    // 입력된 이름을 저장
    // 저장된 이름과 타임을 랭킹에 출력

    public void SaveButtonClick()
    {
        string playerName = inputFieldUI.text;
        Debug.Log($"플레이어 이름 : {playerName}");
        
        SaveManager.Inst.SaveDate(playerName);
    }
    
    public void CancelButtonClick()
    {
        // SceneManager.Inst.LoadScene(타이틀 씬으로 전환)
    }
}
