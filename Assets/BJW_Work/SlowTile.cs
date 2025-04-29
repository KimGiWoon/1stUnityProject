using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTile : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f; // 느려지는 비율 (0.5배)

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = 5f; // 드래그를 올려서 느려지게 만든다
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
                rb.drag = 0f; // 드래그를 원래대로 복구
            }
        }
    }
}
