using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] int grabRange; // ��� ����
    [SerializeField] int grabbableLayer; // ���� �� �ִ� ���̾�
    public KeyCode grabKey = KeyCode.E; // ��� Ű ��ȣ�ۿ�

    private FixedJoint grabJoint; // �罽? ��? ���� ������ ���
    private Rigidbody grabbedBody; // ���� ��� �ִ°� ���� ������ ����



    void Update()
    {

        if (Input.GetKeyDown(grabKey)) // �׷� Ű �Է� e ������
        {
            if (grabJoint == null) // ��� ���� ��
            {
                TryGrab();
            }
            else // �ƴ� ��
            {
                Release();
            }
        }
    }


    void TryGrab()
    {
        RaycastHit isObject; // ����ĳ��Ʈ�� ������Ʈ �Ǻ�
        if (Physics.Raycast(transform.position, transform.forward, out isObject, grabRange, grabbableLayer))
        {
            if (isObject.rigidbody != null) // ������Ʈ�� ���� �ۿ��� ������
            {
                grabbedBody = isObject.rigidbody; // ��� �ִ� ������ ���� �� �ִ� ������Ʈ��� ������ �ٵ� ����

                grabJoint = gameObject.AddComponent<FixedJoint>(); // ��⸦ �ٿ���.
                grabJoint.connectedBody = grabbedBody;
                grabJoint.breakForce = 500f;  // ���� �ʹ� ���� �ָ� �ڵ����� ������ ���� ����
                grabJoint.breakTorque = 500f; // ��ũ�� �ʹ� ���� �ڵ����� ������
            }
        }
    }



    void Release()
    {
        if (grabJoint != null) // ��� ���� �� ������
        {
            Destroy(grabJoint); // ��� �ִ��� Ǭ��.
            grabJoint = null; // null�� �⺻�� �ǵ�����.
            grabbedBody = null; // �����ߴ� ���� ��Ȱ��ȭ.
        }
    }


}
