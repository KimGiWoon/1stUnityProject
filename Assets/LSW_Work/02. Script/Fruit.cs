using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Number")]
    [Tooltip("포탄(과일) 지속 시간")] 
    [SerializeField] private float liveTime;

    private float _timer;
    private FruitPool _pool;
    public FruitPool Pool
    {
        get { return _pool; }
        set { _pool = value; }
    }
    
    private void OnEnable()
    {
        Init();
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        Return();
    }

    private void Return()
    {
        if (_timer > liveTime)
        {
            if (_pool == null)
            {
                Destroy(gameObject);
            }
            else
            {
                _pool.ReturnPool(this);
            }

            _timer = 0f;
        }
    }

    private void Init()
    {
        _timer = 0f;
        if (liveTime == 0)
        {
            liveTime = 5f;
        }
    }
}
