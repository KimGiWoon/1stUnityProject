using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float liveTime;
    public FruitPool pool;
    private float _timer;

    private void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        _timer += Time.deltaTime;
        Return();
    }

    private void Return()
    {
        if (_timer > liveTime)
        {
            if (pool == null)
            {
                Destroy(gameObject);
            }
            else
            {
                pool.ReturnPool(this);
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
