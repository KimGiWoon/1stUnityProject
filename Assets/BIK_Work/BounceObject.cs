using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour {
    #region serialized fields

    [Tooltip("Æ¨±â´Â ¼¼±â")]
    [SerializeField] private float bounceForce = 10f;

    #endregion // serialized fields





    #region mono funcs

    private void OnCollisionEnter(Collision collision) {
        Rigidbody otherRb = collision.rigidbody;

        if (otherRb != null) {
            if (collision.contacts.Length > 0) {
                ContactPoint contact = collision.contacts[0];

                Vector3 direction = (contact.point - transform.position).normalized;

                otherRb.velocity = Vector3.zero;

                otherRb.AddForce(direction * bounceForce, ForceMode.VelocityChange);
            }
        }
    }

    #endregion // mono funcs
}
