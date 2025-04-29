using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour
{
    [SerializeField] private float iceDrag = 0f; // ���� �������� 0
    private Dictionary<Rigidbody, float> originalDrags = new Dictionary<Rigidbody, float>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (!originalDrags.ContainsKey(rb))
                    originalDrags[rb] = rb.drag; // ���� �� ���� drag ����
                rb.drag = iceDrag; // ���� �������� �̲�����
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = iceDrag;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null && originalDrags.ContainsKey(rb))
            {
                rb.drag = originalDrags[rb]; // ���� �� ���� drag ����
                originalDrags.Remove(rb); // �����ߴ� ������ ����
            }
        }
    }
}
