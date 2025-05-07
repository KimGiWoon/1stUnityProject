using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;      // ���� �÷��̾�
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0); // ī�޶� �ʱ� ��ġ
    [SerializeField][Range(5, 20)] float followSpeed = 10f;      // �÷��̾� ���󰡴� �ӵ� �ʱⰪ
    [SerializeField][Range(10, 100)] float rotateSpeed = 20f;     // ���콺 ȸ�� �ӵ� �ʱⰪ
    [SerializeField][Range(10, 30)] float zoomInSpeed = 10f;     // ���콺 ���� �ӵ� �ʱⰪ
    [SerializeField] float cameraPosY = 3f; // ī�޶� y�� �ʱ���ġ
    float curRotationX = 0f;     // ���� X����ġ �ʱ�ȭ
    float curRotationY = 7f;     // ���� Y����ġ �ʱ�ȭ
    float curRotationZ = -10f;     // ���� Y����ġ �ʱ�ȭ
    
    private void Update()
    {
        CameraMoveInput();
    }

    // �÷��̾ �����̰� ���� ī�޶�� Ȯ���ؾ� �ؼ� LateUpdate���� ����
    private void LateUpdate()
    {
        CameraMove();
    }

    // ī�޶� ��ġ �Է�
    private void CameraMoveInput()
    {
        // ���콺 Ŀ���� ��ġ�� ī�޶� ȸ��
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");
        float mouseMoveZ = Input.GetAxis("Mouse ScrollWheel");
        
        curRotationY += mouseMoveX * rotateSpeed * 10 * Time.deltaTime;
        curRotationX += mouseMoveY * rotateSpeed * 10 * Time.deltaTime;
        curRotationZ += mouseMoveZ * zoomInSpeed * 50 * Time.deltaTime;
    }
    // ī�޶� ������
    private void CameraMove()
    {
        // �÷��̾� ��ġ + ������ (ī�޶� X, Y��)
        transform.rotation = Quaternion.Euler(curRotationX * (-1) , curRotationY, 0);    // X�� �̵� ���콺 �������� ī�޶� �Ĵٺ���
        // ī�޶� ����
        offset.z = curRotationZ;
        Vector3 movePosition = target.position + transform.rotation * offset;

        // �ε巴�� �̵� (Lerp ���)
        transform.position = Vector3.Lerp(transform.position, movePosition, followSpeed * Time.deltaTime);

        // �׻� �÷��̾� �ٶ󺸱�
        transform.LookAt(target.position + Vector3.up * cameraPosY); // ī�޶� ������ ������ �ٶ󺸰�
    }
}
