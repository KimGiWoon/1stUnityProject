using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] LayerMask grabbableLayer; // 잡을 수 있는 레이어 (벽 잡기용)
    [SerializeField] float climbJumpForce = 2f; // 오르는 힘
    [SerializeField] float climbSpeed = 2f; // 클라이밍 스피드
    [SerializeField] float climbRange = 0.05f; // 클라이밍 범위
    [SerializeField] private Transform grabPoint; // 손위치
    public KeyCode grabKey = KeyCode.E; // 잡는 키 상호작용
    public KeyCode jumpKey = KeyCode.Space; // 점프 키 상호작용
    private float grabStartY; // 벽 잡은 순간 플레이어 높이 저장
    private GameObject grabCandidate = null;
    public bool IsGrabbing => isGrab; // 읽기 전용 프로퍼티
    private Animator animator;
    private bool isGrab = false;
    private Rigidbody myBody;
    private GameObject grabbedKey = null;

    public bool IsWallGrabbing => isGrab && grabbedKey == null; // 그랩 상태이면서 키를 안 잡고 있을 때만 벽 잡기로 간주

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 열쇠를 잡고 있을 때, 손 앞 위치로 따라오게 함 (자연스럽게 이동)
        if (grabbedKey != null)
        {
            // grabPoint의 월드 위치를 계산하여 부드럽게 이동
            Vector3 targetPos = grabPoint.position + grabPoint.forward * 0.1f;

            // 자연스러운 이동과 회전을 위해 Lerp 속도 조정
            grabbedKey.transform.position = Vector3.Lerp(grabbedKey.transform.position, targetPos, 0.2f);
            grabbedKey.transform.rotation = Quaternion.Lerp(grabbedKey.transform.rotation, grabPoint.rotation, 0.2f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(grabKey) && grabCandidate != null)
        {
            GrabKey(grabCandidate);
        }

        if (Input.GetKeyUp(grabKey))
        {
            UnTryGrab(); // 그랩 해제
        }

        if (isGrab && grabbedKey == null)
        {
            Climbing();

            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
    }

    void UnTryGrab()
    {
        if (isGrab) // 그랩 상태이면
        {
            isGrab = false; // 그랩 해제
            myBody.useGravity = true; // 중력 다시 적용
        }

        if (grabbedKey != null) // 키를 잡고 있었다면
        {
            // 부모 해제 전에 현재 위치를 고정하여 튀는 현상 방지
            grabbedKey.transform.SetParent(null);
            Rigidbody keyRb = grabbedKey.GetComponent<Rigidbody>();
            if (keyRb != null)
            {
                keyRb.useGravity = true;
                keyRb.isKinematic = false;
            }

            grabbedKey = null;
        }
    }

    void TryWallGrab(Collider wallCollider)
    {
        isGrab = true;
        myBody.useGravity = false;
        myBody.velocity = Vector3.zero;
        grabStartY = transform.position.y;

        // 카메라 방향으로 몸 회전
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        if (camForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(camForward);
        }
    }

    void GrabKey(GameObject keyObj)
    {
        isGrab = true; // 벽이든 키든 잡았을 때는 true
        grabbedKey = keyObj;

        myBody.useGravity = true;
        myBody.velocity = Vector3.zero;

        Rigidbody keyRb = keyObj.GetComponent<Rigidbody>();
        if (keyRb != null)
        {
            keyRb.useGravity = false;
            keyRb.isKinematic = true; // 움직임 직접 제어
            keyRb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        // 키를 grabPoint의 자식으로 설정하여 위치 고정
        grabbedKey.transform.SetParent(grabPoint);
        grabbedKey.transform.localPosition = new Vector3(0, 0, 0.1f); // 손 앞 0.1f 위치
        grabbedKey.transform.localRotation = Quaternion.identity; // 회전 초기화

    }

    void Climbing()
    {
        float verticalLook = Camera.main.transform.forward.y; // 카메라 y축 방향값
        float currentY = transform.position.y; // 현재 y 위치

        float minY = grabStartY - climbRange;
        float maxY = grabStartY + climbRange;

        // 아직 벽을 붙잡고 있는지 확인
        RaycastHit hit;
        Vector3 rayOrigin = grabPoint.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (!Physics.Raycast(rayOrigin, rayDirection, out hit, 0.5f, grabbableLayer))
        {
            UnTryGrab(); // 벽 놓기
            return;
        }

        // 아래로 이동
        if (verticalLook > 0.2f && currentY > minY)
        {
            Vector3 move = -Vector3.up * climbSpeed * Time.deltaTime;
            if (transform.position.y + move.y < minY)
                move.y = minY - transform.position.y;

            myBody.MovePosition(transform.position + move);
        }
        // 위로 이동
        else if (verticalLook < -0.2f && currentY < maxY)
        {
            Vector3 move = Vector3.up * climbSpeed * Time.deltaTime;
            if (transform.position.y + move.y > maxY)
                move.y = maxY - transform.position.y;

            myBody.MovePosition(transform.position + move);
        }
    }

    void WallJump() // 벽 잡은 상태에서 점프
    {
        isGrab = false;
        myBody.useGravity = true;
        myBody.velocity = new Vector3(myBody.velocity.x, climbJumpForce, myBody.velocity.z);
        animator.SetBool("Jump", true);
        animator.SetBool("OnGround", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (grabbedKey != null) return; // 이미 키를 잡고 있으면 무시

        if (other.CompareTag("Key")) // 키 오브젝트라면
        {
            grabCandidate = other.gameObject; // 입력 대기 중인 키 저장
        }
        else if (((1 << other.gameObject.layer) & grabbableLayer) != 0) // 벽 레이어라면
        {
            TryWallGrab(other); // 벽 잡기
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == grabCandidate)
        {
            grabCandidate = null;
        }
    }

}