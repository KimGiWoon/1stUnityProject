using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class WaterMovement : MonoBehaviour
{
    [Header("Drag&Drop")]
    [Tooltip("스폰 위치, 하이어라키 창에서 빈 오브젝트 생성 후 position을 (0,-20,0)로 설정")]
    [SerializeField] private Transform spawnPos;
    
    [Header("Number")]
    [Tooltip("물이 올라오는 속도")]
    [SerializeField] private float upSpeed;
    
    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        Move();
    }
 
    private void Move()
    {
        transform.Translate(Vector3.up * (upSpeed * Time.deltaTime));
    }

    private void Init()
    {
        transform.position = spawnPos.position;
    }
}
