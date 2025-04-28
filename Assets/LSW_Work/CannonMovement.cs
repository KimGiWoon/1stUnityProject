using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private FruitPool pool;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float coolTime;
    
    private Coroutine _fireRoutine;
    private WaitForSeconds _fireCool;
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _fireRoutine = StartCoroutine(FireRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_fireRoutine);
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
