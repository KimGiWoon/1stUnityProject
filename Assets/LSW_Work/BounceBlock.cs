using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BounceBlock : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float force;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        if (rigid != null)
        {
            Debug.Log("충돌 진입");
            ContactPoint contact = other.contacts[0];
            Vector3 forceDir = contact.normal;

            Vector3 velocity = rigid.velocity;
            Vector3 reflectedVelocity = Vector3.Reflect(velocity,forceDir);
            rigid.velocity = reflectedVelocity.normalized * force;
        }
    }
}
