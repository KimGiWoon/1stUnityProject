using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class WaterMovement : MonoBehaviour
{
    // 물이 올라오는 속도 : 인스펙터에서 조절 가능 
    [SerializeField] private float upSpeed;
    // 스폰 위치. 하이어라키 창에서 빈 오브젝트 생성 후 position을 (0,-20,0)로 설정하고 드래그.
    [SerializeField] private Transform spawnPos;
    // Update is called once per frame

    /// <summary>
    /// 활성화되면 스폰 지점에서부터 upSpeed에 따라 위로 올라오도록 구현
    /// </summary>
    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        Move();
    }
    
    /// <summary>
    /// 위로 올라오는 함수
    /// </summary>
    private void Move()
    {
        transform.Translate(Vector3.up * (upSpeed * Time.deltaTime));
    }
    
    /// <summary>
    /// 초기화 함수. 스폰 위치 지정
    /// </summary>
    private void Init()
    {
        transform.position = spawnPos.position;
    }
}
