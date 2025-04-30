using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeightCalculate : MonoBehaviour
{
    [Header("Drag&Drop")] 
    [Tooltip("InGameUICanvas의 하위 객체 드래그")] 
    [SerializeField] private TextMeshProUGUI heightText;
    [Tooltip("Player")]
    [SerializeField] private Transform playerTrans;
    [Tooltip("1F - GameObjects - Water")]
    [SerializeField] private Transform waterTrans;
    
    private void Update()
    {
        float height = playerTrans.position.y - waterTrans.position.y;
        heightText.text = $"Height : {height:00.0}m";
    }
}
