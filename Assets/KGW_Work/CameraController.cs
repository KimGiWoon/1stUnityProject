using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;      // 따라갈 플레이어
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0); // 카메라 초기 위치
    [SerializeField][Range(5, 20)] float followSpeed = 10f;      // 플레이어 따라가는 속도 초기값
    [SerializeField][Range(10, 100)] float rotateSpeed = 20f;     // 마우스 회전 속도 초기값
    [SerializeField][Range(10, 30)] float zoomInSpeed = 10f;     // 마우스 줌인 속도 초기값
    [SerializeField] float cameraPosY = 3f; // 카메라 y축 초기위치
    float curRotationX = 0f;     // 현재 X축위치 초기화
    float curRotationY = 7f;     // 현재 Y축위치 초기화
    float curRotationZ = -10f;     // 현재 Y축위치 초기화
    
    private void Update()
    {
        CameraMoveInput();
    }

    // 플레이어가 움직이고 나서 카메라로 확인해야 해서 LateUpdate에서 구현
    private void LateUpdate()
    {
        CameraMove();
    }

    // 카메라 위치 입력
    private void CameraMoveInput()
    {
        // 마우스 커서의 위치로 카메라 회전
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");
        float mouseMoveZ = Input.GetAxis("Mouse ScrollWheel");
        
        curRotationY += mouseMoveX * rotateSpeed * 10 * Time.deltaTime;
        curRotationX += mouseMoveY * rotateSpeed * 10 * Time.deltaTime;
        curRotationZ += mouseMoveZ * zoomInSpeed * 50 * Time.deltaTime;
    }
    // 카메라 움직임
    private void CameraMove()
    {
        // 플레이어 위치 + 오프셋 (카메라 X, Y축)
        transform.rotation = Quaternion.Euler(curRotationX * (-1) , curRotationY, 0);    // X축 이동 마우스 방향으로 카메라 쳐다보게
        // 카메라 줌인
        offset.z = curRotationZ;
        Vector3 movePosition = target.position + transform.rotation * offset;

        // 부드럽게 이동 (Lerp 사용)
        transform.position = Vector3.Lerp(transform.position, movePosition, followSpeed * Time.deltaTime);

        // 항상 플레이어 바라보기
        transform.LookAt(target.position + Vector3.up * cameraPosY); // 카메라 방향을 위에서 바라보게
    }
}
