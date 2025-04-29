using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FruitPool : MonoBehaviour
{
    [Header("Drag&Drop")]
    [Tooltip("포탄(과일) 프리팹 지정")] 
    [SerializeField] private Fruit fruitPrefab;
    
    [Header("Number")]
    [Tooltip("풀 크기, spawn을 3개 이상 배치할 경우 사이즈업")] 
    [SerializeField] private int poolSize;
    // 오브젝트 풀 Queue로 구현
    private Queue<Fruit> _fruits;
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 오브젝트 풀에서 포탄(과일)을 불러오는 함수
    /// </summary>
    /// <returns>새롭게 생성하거나, 풀에서 가져온 포탄(과일)</returns>
    public Fruit GetPool()
    {
        // 큐에 꺼낼 수 있는 과일이 없는 경우
        if (_fruits.Count == 0)
        {
            // 새로 인스턴스 생성
            Fruit instance = Instantiate(fruitPrefab);
            return instance;
        }
        // 큐에서 꺼낼 수 있는 과일이 있는 경우
        else
        {
            // 큐에서 과일을 꺼내 인스턴스 지정
            Fruit instance = _fruits.Dequeue();
            // 해당 인스턴스 활성화
            instance.gameObject.SetActive(true);
            return instance;
        }
    }

    /// <summary>
    /// 포탄(과일)을 오브젝트 풀로 반환하는 함수. 반환할 풀이 없는 경우 포탄(과일)을 파괴.
    /// </summary>
    /// <param name="fruit">파괴하거나, 반환할 포탄(과일)</param>
    public void ReturnPool(Fruit fruit)
    {
        // 큐에서 꺼내지 않고 새로 생성되어 지정된 pool이 없는 경우
        if (fruit.Pool == null)
        {
            // 풀에 반환하지 않고 파괴
            Destroy(fruit.gameObject);
        }
        // 큐에서 꺼낸 경우
        else
        {
            // 해당 인스턴스 비활성화, 이후 풀에 반환
            fruit.gameObject.SetActive(false);
            _fruits.Enqueue(fruit);
        }
    }

    // 오브젝트 풀 초기화
    private void Init()
    {
        _fruits = new Queue<Fruit>();
        for (int i = 0; i < poolSize; i++)
        {
            Fruit instance = Instantiate(fruitPrefab);
            instance.gameObject.SetActive(false);
            // 과일의 pool 지정을 여기서
            instance.Pool = this;
            _fruits.Enqueue(instance);
        }
    }
}
