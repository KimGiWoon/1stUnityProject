using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareWallMovement : MonoBehaviour
{
    // 하위 객체인 mainWall 드래그 앤 드롭
    [SerializeField] private Transform mainWall;
    // 벽의 이동속도 - 인스펙터에서 조절 가능
    [SerializeField] private float moveSpeed;
    // 벽의 운동 주기
    private readonly float _moveTimer = 2f;
    // 흐름 제어를 위한 내부 캐싱
    private Coroutine _moveRoutine;

    /// <summary>
    /// 생성시 초기화 및 무브 코루틴 작동하도록 구현
    /// </summary>
    private void Awake()
    {
        Init();
        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    /// <summary>
    /// 파괴되거나 비활성화시키는 경우 기존의 코루틴 종료
    /// </summary>
    private void OnDisable()
    {
        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
        }
    }
    
    /// <summary>
    /// _moveTimer(2초)를 주기로 앞뒤로 움직이는 메인 로직(코루틴)
    /// </summary>
    /// <returns>프레임 단위로 움직이기 위한 값</returns>
    private IEnumerator MoveRoutine()
    {
        float timer = 0f;
        
        while (true)
        {
            while(timer < _moveTimer)
            {
                timer += Time.deltaTime;
                MoveForward();
                yield return null;
            }
            timer = 0f;
            
            while(timer < _moveTimer)
            {
                timer += Time.deltaTime;
                MoveBack();
                yield return null;
            }
            timer = 0f;
        }
    }
    
    private void MoveForward()
    {
        // is kinetic 설정을 활용하기 위한 Translate 사용
        mainWall.transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
    }
    
    private void MoveBack()
    {
        // is kinetic 설정을 활용하기 위한 Translate 사용
        mainWall.transform.Translate(Vector3.back * (moveSpeed * Time.deltaTime));
    }

    private void Init()
    {
        // 초기화 조건(생기는 경우)
    }
}
