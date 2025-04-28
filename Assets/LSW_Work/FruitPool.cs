using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FruitPool : MonoBehaviour
{
    [SerializeField] private Fruit fruitPrefab;
    [SerializeField] private int poolSize;

    private Queue<Fruit> _fruits;
    private void Awake()
    {
        Init();
    }

    public Fruit GetPool()
    {
        if (_fruits.Count == 0)
        {
            Fruit instance = Instantiate(fruitPrefab);
            return instance;
        }
        else
        {
            Fruit instance = _fruits.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }
    }

    public void ReturnPool(Fruit fruit)
    {
        if (fruit.pool == null)
        {
            Destroy(fruit.gameObject);
        }
        else
        {
            fruit.gameObject.SetActive(false);
            _fruits.Enqueue(fruit);
        }
    }

    private void Init()
    {
        _fruits = new Queue<Fruit>();
        for (int i = 0; i < poolSize; i++)
        {
            Fruit instance = Instantiate(fruitPrefab);
            instance.gameObject.SetActive(false);
            instance.pool = this;
            _fruits.Enqueue(instance);
        }
    }
}
