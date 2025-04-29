using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CannonMovement : MonoBehaviour
{
    [Header("Drag&Drop")]
    [Tooltip("포탄(과일) 오브젝트 풀 지정")] 
    [SerializeField] private FruitPool pool;
    
    [Header("Number")]
    [Tooltip("포탄(과일)이 날아가는 속도")] 
    [SerializeField] private float moveSpeed;
    [Tooltip("포탄(과일) 발사 쿨타임")] 
    [SerializeField] private float coolTime;
    
    private Coroutine _fireRoutine;
    private WaitForSeconds _fireCool;
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        if (_fireRoutine == null)
        {
            _fireRoutine = StartCoroutine(FireRoutine());
        }
        
    }
    
    // 이 부분 null 참조 오류 해결하고 추가해야함(씬 전환 때 활용할 수 있도록)
    /*private void OnEnable()
    {
        if (_fireRoutine == null)
        {
            _fireRoutine = StartCoroutine(FireRoutine());
        }
    }*/

    private void OnDisable()
    {
        StopCoroutine(_fireRoutine);
        _fireRoutine = null;
    }
    

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            Fire();
            yield return _fireCool;
        }
    }
    
    private void Fire()
    {
        Fruit instance = pool.GetPool();
        instance.transform.position = transform.position;
        instance.GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
    }

    private void Init()
    {
        if (coolTime == 0)
        {
            coolTime = 5f;
        }
        _fireCool = new WaitForSeconds(coolTime);
    }
}
