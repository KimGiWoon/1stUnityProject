using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BounceBlock : MonoBehaviour
{
    [Header("Drag&Drop")]
    [Tooltip("플레이어 레이어 지정")] 
    [SerializeField] private LayerMask playerLayer;

    [Header("Number")]
    [Tooltip("밀쳐(튕겨)내는 힘")] 
    [SerializeField] private float force;
    
    // 플레이어의 진입 방향의 반대로 튕겨내도록 구현
    private void OnCollisionEnter(Collision collision) 
    {
        Rigidbody otherRb = collision.rigidbody;

        if (otherRb != null) {
            if (collision.contacts.Length > 0) {
                ContactPoint contact = collision.contacts[0];

                Vector3 direction = (contact.point - transform.position).normalized;

                otherRb.velocity = Vector3.zero;

                otherRb.AddForce(direction * force, ForceMode.VelocityChange);
            }
        }
    }
}
