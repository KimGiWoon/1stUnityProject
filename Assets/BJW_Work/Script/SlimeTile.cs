using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SlimeTile : MonoBehaviour
{
    [SerializeField] private float slowDrag = 5f; // ������ ������ drag
    [SerializeField] private float restoreDrag = 0f; // ������ ����� �� ������ �⺻ drag

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = slowDrag; // ������ ������ ������
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ���� �÷��̾ �����ϰ� �ִ��� "������ Ÿ��"�� ���� ������� ����
                if (IsStillOnSlimeTile(rb))
                {
                    rb.drag = restoreDrag; // ������ Ÿ�ϸ� ����� ����
                }
            }
        }
    }

    private bool IsStillOnSlimeTile(Rigidbody rb)
    {
        // rb�� ���� �ٸ� ������ Ÿ�Ͽ� ��������� �������� �ʴ´�.
        Collider[] hits = Physics.OverlapSphere(rb.position, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("SlimeTile")) // ������ Ÿ���̸�
            {
                return false; // ���� ������ ���� ����
            }
        }
        return true; // �� �̻� ������ ���� �ƴ�
    }
}
