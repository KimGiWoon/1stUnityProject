using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareWallMovement : MonoBehaviour
{
    [SerializeField] private Transform mainWall;
    [SerializeField] private float moveSpeed;
    private readonly float _moveTimer = 2f;
    private Coroutine _moveRoutine;

    private void Awake()
    {
        Init();
        Debug.Log("루틴 시작");
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
        mainWall.transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        Debug.Log("전방 이동");
    }
    
    private void MoveBack()
    {
        mainWall.transform.Translate(Vector3.back * (moveSpeed * Time.deltaTime));
        Debug.Log("후방 이동");
    }

    private void Init()
    {
        
    }
}
