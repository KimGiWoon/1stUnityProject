using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;      // ���� �÷��̾�
    [SerializeField] Vector3 offset = new Vector3(0, 10, -13); // ī�޶� �ʱ� ��ġ
    [SerializeField][Range(10, 50)] float followSpeed = 10f;      // �÷��̾� ���󰡴� �ӵ� �ʱⰪ
    [SerializeField][Range(100, 500)] float rotateSpeed = 200f;     // ���콺 ȸ�� �ӵ� �ʱⰪ
    [SerializeField] float cameraPosY = 4f; // ī�޶� y�� �ʱ���ġ
    float curRotationX = 0f;     // ���� X����ġ �ʱ�ȭ
    float curRotationY = 0f;     // ���� Y����ġ �ʱ�ȭ
    
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
        // ���콺 �Է����� ī�޶� ȸ��
        if (Input.GetMouseButton(1))
        {
            float mouseMoveX = Input.GetAxis("Mouse X");
            float mouseMoveY = Input.GetAxis("Mouse Y");
            curRotationY += mouseMoveX * rotateSpeed * Time.deltaTime;
            curRotationX += mouseMoveY * rotateSpeed * Time.deltaTime;
        }
    }
    // ī�޶� ������
    private void CameraMove()
    {
        // �÷��̾� ��ġ + ������ (ī�޶� X, Y��)
        transform.rotation = Quaternion.Euler(curRotationX, curRotationY, 0);
        Vector3 movePosition = target.position + transform.rotation * offset;

        // �ε巴�� �̵� (Lerp ���)
        transform.position = Vector3.Lerp(transform.position, movePosition, followSpeed * Time.deltaTime);

        // �׻� �÷��̾� �ٶ󺸱�
        transform.LookAt(target.position + Vector3.up * cameraPosY); // ī�޶� ������ ������ �ٶ󺸰�
    }
}
