using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ClearSceneUI : MonoBehaviour
{
    [SerializeField] TMP_InputField inputFieldUI;
    [SerializeField] Transform rankList;   // Content 오브젝트
    [SerializeField] GameObject rankListPrefab;   // RankingItem 프리팹
    bool OnsaveButton = false;

    private void Start()
    {
        RankListUI();    // 랭킹 UI 새로고침
    }

    public void OnClickSaveButton()
    {
        if(!OnsaveButton)
        {
            string playerName = inputFieldUI.text;
            SaveManager.Inst.SaveDate(playerName);
            // TODO 입력된 플레이어이름 및 플레이타임 출력 -> 확인용으로 추후 삭제 계획
            //Debug.Log(SaveManager.Inst.GetData());
            //Debug.Log(SaveManager.Inst.GetPlayTime());

            float clearTime = SaveManager.Inst.GetPlayTime();  // 현재 플레이 타임 가져오기
            SaveManager.Inst.SaveRankingData(playerName, clearTime);  // 랭킹에 반영

            RankListUI();    // 랭킹 UI 새로고침
            OnsaveButton = true;
        }
    }

    // 취소 버튼 클릭
    public void OnClickBackButton()
    {
        SceneManager.Inst.LoadTitleScene();
        // 취소 버튼 클릭 시 점수 저장하지 않고 타이틀 씬으로 전환
        OnsaveButton = false;
    }

    // 랭킹 리스트 초기화 버튼
    public void OnClickClearButton()
    {
        SaveManager.Inst.ClearRankingData();    // 클리어 함수 호출
        RankListUI();  // 랭킹 UI 새로고침
        OnsaveButton = false;
    }

    // 랭킹 리스트 
    public void RankListUI()
    {
        // 기존 항목 제거
        foreach (Transform ranking in rankList)
        {
            Destroy(ranking.gameObject);
        }

        // 저장한 랭킹 리스트 가져오기
        List<(string name, float time)> rankingList = SaveManager.Inst.GetRankingData();

        // 텍스트에 순위, 플레이어 이름, 플레이 타임 출력
        for (int i = 0; i < rankingList.Count; i++)
        {
            GameObject item = Instantiate(rankListPrefab, rankList);

            TMP_Text rankText = item.transform.Find("RankText").GetComponent<TMP_Text>();
            TMP_Text nameText = item.transform.Find("NameText").GetComponent<TMP_Text>();
            TMP_Text timeText = item.transform.Find("TimeText").GetComponent<TMP_Text>();

            rankText.text = $"{i + 1}";
            nameText.text = rankingList[i].name;
            timeText.text = $"{rankingList[i].time:F2}";
        }
    }


}
