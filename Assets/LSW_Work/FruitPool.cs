using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FruitPool : MonoBehaviour
{
    // 포탄(과일) 프리팹 지정
    [SerializeField] private Fruit fruitPrefab;
    // 풀 크기, 5개 이상 배치할 경우 사이즈 늘려야할 필요o
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
    /// <returns></returns>
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

    public void ReturnPool(Fruit fruit)
    {
        // 큐에서 꺼내지 않고 새로 생성되어 지정된 pool이 없는 경우
        if (fruit.pool == null)
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

    // 오브젝트 풀 초기화 함수
    private void Init()
    {
        _fruits = new Queue<Fruit>();
        for (int i = 0; i < poolSize; i++)
        {
            Fruit instance = Instantiate(fruitPrefab);
            instance.gameObject.SetActive(false);
            // 과일의 pool 지정을 여기서
            instance.pool = this;
            _fruits.Enqueue(instance);
        }
    }
}
