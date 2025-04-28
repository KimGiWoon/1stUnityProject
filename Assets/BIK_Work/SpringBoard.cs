using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour {
    #region serialized fields

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _playerLayer;

    #endregion // serialized fields





    #region private variables

    private Rigidbody _rigidbody;

    #endregion // private variables





    #region mono funcs

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (((1 << collision.gameObject.layer) & _playerLayer) != 0) {
            Jump();
        }
    }

    #endregion // mono funcs





    #region private funcs

    private void Jump() {
        if (_rigidbody != null) {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    #endregion // private funcs
}
