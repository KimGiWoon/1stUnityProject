using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTile : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f; // �������� ���� (0.5��)

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = 5f; // �巡�׸� �÷��� �������� �����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = 0f; // �巡�׸� ������� ����
            }
        }
    }
}
