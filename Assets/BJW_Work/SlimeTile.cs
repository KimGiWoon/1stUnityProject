using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTile : MonoBehaviour
{
    [SerializeField] private float slowDrag = 5f; // ������ �� ������ drag ��
    [SerializeField] private float normalDrag = 0f; // ���� drag �� (�⺻�� 0)

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.drag = slowDrag; // �巡�׸� ũ�� �ؼ� �������� ���ϰ� �����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.drag = normalDrag; // �巡�׸� ������� ����
        }
    }
}
