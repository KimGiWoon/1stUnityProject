using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquareWallMovement : MonoBehaviour
{
    [Header("Drag&Drop")]
    [Tooltip("하위 객체 mainWall")]
    [SerializeField] private Transform mainWall;
    
    [Header("Number")]
    [Tooltip("벽의 이동속도")]
    [SerializeField] private float moveSpeed;
    
    // 벽의 운동 주기
    private float _moveTimer;
    // 흐름 제어를 위한 내부 캐싱
    private Coroutine _moveRoutine;
    // 루틴 시작 시간 변수
    private WaitForSeconds _waitTime;
    
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    private void OnDisable()
    {
        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
        }
    }
    
    private IEnumerator MoveRoutine()
    {
        yield return _waitTime;
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
        float num = Random.Range(0,200);
        _waitTime = new WaitForSeconds(num / 100);
        _moveTimer = 4 / moveSpeed;
    }
}
