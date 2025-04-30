using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeightCalculate : MonoBehaviour
{
    [Header("Drag&Drop")] 
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private Transform waterTrans;
    
    
    private void Update()
    {
        float height = playerTrans.position.y - waterTrans.position.y;
        heightText.text = $"Height : {height:00.0}m";
    }
}
