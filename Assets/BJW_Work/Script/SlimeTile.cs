using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTile : MonoBehaviour
{
    [SerializeField] private float slowDrag = 5f; // 느려질 때 적용할 drag 값
    [SerializeField] private float normalDrag = 0f; // 원래 drag 값 (기본은 0)

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.drag = slowDrag; // 드래그를 크게 해서 움직임을 둔하게 만든다
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.drag = normalDrag; // 드래그를 원래대로 복구
        }
    }
}
