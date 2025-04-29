using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour
{
    [SerializeField] private float iceDrag = 0f; // 얼음 위에서는 0
    private Dictionary<Rigidbody, float> originalDrags = new Dictionary<Rigidbody, float>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (!originalDrags.ContainsKey(rb))
                    originalDrags[rb] = rb.drag; // 들어올 때 원래 drag 저장
                rb.drag = iceDrag; // 얼음 위에서는 미끄럽게
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
                rb.drag = originalDrags[rb]; // 나갈 때 원래 drag 복구
                originalDrags.Remove(rb); // 저장했던 정보도 삭제
            }
        }
    }
}
