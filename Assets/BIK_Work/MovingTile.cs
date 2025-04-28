using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {
    #region serialized fields

    [Tooltip("�̵� ���� // X : �¿�, Z : �յ�")]
    [SerializeField] private Vector3 _moveDirection = Vector3.left;
    [Tooltip("�̵� �ӵ�")]
    [SerializeField] private float _moveForce = 100f;
    [Tooltip("�÷��̾� ���̾� // ��� ������Ʈ ���� �����ϰ������ Everything")]
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
