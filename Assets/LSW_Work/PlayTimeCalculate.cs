using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayTimeCalculate : MonoBehaviour
{
    [Header("Drag&Drop")] 
    [Tooltip("InGameUICanvas의 하위 객체 드래그")] 
    [SerializeField] private TextMeshProUGUI heightText;
    // 랭킹 데이터 매니저
    
    
    private void Update()
    {
        heightText.text = $"Time : {SaveManager.Inst.GetPlayTime():F2}";
    }
}
