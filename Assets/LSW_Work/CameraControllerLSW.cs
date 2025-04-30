using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraControllerLSW : MonoBehaviour
{
    [SerializeField] Transform target;      // 따라갈 플레이어
    [SerializeField] Vector3 offset = new Vector3(0f, 2f, -5f); // 카메라 초기 위치
    [SerializeField][Range(5, 20)] float rotateSpeed = 5f;     // 마우스 회전 속도 초기값
    [SerializeField][Range(5, 20)] float followSpeed = 10f;      // 플레이어 따라가는 속도 초기값
    // [SerializeField][Range(10, 30)] float zoomInSpeed = 10f;     // 마우스 줌인 속도 초기값
    // [SerializeField] float cameraPosY = 3f; // 카메라 y축 초기위치
    // private float curRotationZ = 10F;
    private float curRotationX = 10f;     // 현재 X축위치 초기화
    private float curRotationY = 0f;     // 현재 Y축위치 초기화
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
        //float mouseMoveZ = Input.GetAxis("Mouse ScrollWheel");
        
        curRotationY += mouseMoveX * rotateSpeed;
        curRotationX -= mouseMoveY * rotateSpeed;
        //curRotationZ += mouseMoveZ * zoomInSpeed * 50 * Time.deltaTime;
    }
    // 카메라 움직임
    private void CameraMove()
    {
        // 카메라 위 아래 각도 제한
        curRotationX = Mathf.Clamp(curRotationX, -60f, 60f);
        // 플레이어 위치 + 오프셋 (카메라 X, Y축)
        Quaternion rotation = Quaternion.Euler(0, curRotationY, 0);    // X축 이동 마우스 방향으로 카메라 쳐다보게
        // 카메라 줌인
        //offset.z = curRotationZ;
        Vector3 movePosition = target.position + rotation * offset;

        // 부드럽게 이동 (Lerp 사용)
        transform.position = Vector3.Lerp(transform.position, movePosition, followSpeed * Time.deltaTime);

        // 항상 플레이어 바라보기
        transform.LookAt(target.position + Vector3.up * 2f); // 카메라 방향을 위에서 바라보게
    }
}
