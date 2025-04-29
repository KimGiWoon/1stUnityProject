using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SlimeTile : MonoBehaviour
{
    [SerializeField] private float slowDrag = 5f; // 슬라임 위에서 drag
    [SerializeField] private float restoreDrag = 0f; // 슬라임 벗어났을 때 복구할 기본 drag

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = slowDrag; // 슬라임 위에서 느려짐
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
                // 현재 플레이어가 접촉하고 있던게 "슬라임 타일"일 때만 원래대로 복구
                if (IsStillOnSlimeTile(rb))
                {
                    rb.drag = restoreDrag; // 슬라임 타일만 벗어나면 복구
                }
            }
        }
    }

    private bool IsStillOnSlimeTile(Rigidbody rb)
    {
        // rb가 아직 다른 슬라임 타일에 닿아있으면 복구하지 않는다.
        Collider[] hits = Physics.OverlapSphere(rb.position, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("SlimeTile")) // 슬라임 타일이면
            {
                return false; // 아직 슬라임 위에 있음
            }
        }
        return true; // 더 이상 슬라임 위가 아님
    }
}
