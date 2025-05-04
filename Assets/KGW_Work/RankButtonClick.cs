using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class RankButtonClick : MonoBehaviour
{
    [SerializeField] Transform rankBoard;   // Content 오브젝트
    [SerializeField] GameObject rankBoardPrefab;   // RankingItem 프리팹
    [SerializeField] GameObject rankBoardPanel;

    public void RankButtonOn()
    {
        rankBoardPanel.SetActive(true);
        Debug.Log("랭크버튼이 눌렸습니다.");
        RankListUI();
    }

    public void CloseButtonOn()
    {
        rankBoardPanel.SetActive(false);
    }

    public void RankListUI()
    {
        // 기존 항목 제거
        foreach (Transform ranking in rankBoard)
        {
            Destroy(ranking.gameObject);
        }

        // 저장한 랭킹 리스트 가져오기
        List<(string name, float time)> rankingList = SaveManager.Inst.GetRankingData();

        // 텍스트에 순위, 플레이어 이름, 플레이 타임 출력
        for (int i = 0; i < rankingList.Count; i++)
        {
            GameObject item = Instantiate(rankBoardPrefab, rankBoard);

            TMP_Text rankText = item.transform.Find("RankText").GetComponent<TMP_Text>();
            TMP_Text nameText = item.transform.Find("NameText").GetComponent<TMP_Text>();
            TMP_Text timeText = item.transform.Find("TimeText").GetComponent<TMP_Text>();

            rankText.text = $"{i + 1}";
            nameText.text = rankingList[i].name;
            timeText.text = $"{rankingList[i].time:F2}";
        }
    }
}
