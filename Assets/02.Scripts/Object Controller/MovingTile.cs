using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {
    #region serialized fields

    [Tooltip("이동 방향 // X : 좌우, Z : 앞뒤")]
    [SerializeField] private Vector3 _moveDirection = Vector3.left;
    [Tooltip("이동 속도")]
    [SerializeField] private float _moveForce = 100f;
    [Tooltip("플레이어 레이어 // 닿는 오브젝트 전부 적용하고싶으면 Everything")]
    [SerializeField] private LayerMask _playerLayer;

    #endregion // serialized fields





    #region mono funcs

    private void OnCollisionStay(Collision collision) {
        if (((1 << collision.gameObject.layer) & _playerLayer) != 0) {
            Rigidbody rb = collision.rigidbody;
            if (rb != null) {
                Vector3 force = _moveDirection.normalized * _moveForce;
                rb.AddForce(force, ForceMode.Force);
            }
        }
    }

    #endregion // mono funcs
}
