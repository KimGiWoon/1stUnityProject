using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] int grabRange; // 잡는 범위
    [SerializeField] LayerMask grabbableLayer; // 잡을 수 있는 레이어
    [SerializeField] float climbJumpForce = 2f; // 오르는 힘
    [SerializeField] float climbSpeed = 2f; // 클라이밍 스피드
    [SerializeField] float climbRange = 0.05f; // 클라이밍 범위
    public KeyCode grabKey = KeyCode.E; // 잡는 키 상호작용
    public KeyCode jumpKey = KeyCode.Space; // 점프 키 상호작용
    private float grabStartY; // 벽 잡은 순간 플레이어 높이 저장
    public bool IsGrabbing => isGrab; // 읽기 전용 프로퍼티
    [SerializeField] private Transform grabPoint; // 손위치

    private Animator animator;
    private bool isGrab = false;
    private Rigidbody myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Transform cam = Camera.main.transform;
        Vector3 rayOrigin = cam.position;
        Vector3 rayDirection = cam.forward;
        DrawGrabRay(); // 확인용 레이 그리기

        if (Input.GetKeyDown(grabKey)) // 키 입력 받을 때
        {
            TryGrab();
        }

        if (Input.GetKeyUp(grabKey)) // 키 입력을 떼면
        {
            UnTryGrab();
        }


        if (isGrab) // 그랩 상태 일 때
        {
            Climbing();

            // 벽 잡은 상태에서도 점프
            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
    }

    void TryGrab()
    {

        RaycastHit isObject; // 레이캐스트로 오브젝트 판별
        Vector3 rayOrigin = grabPoint.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out isObject, grabRange, grabbableLayer))
        {
            float hitY = isObject.point.y;
            float myY = transform.position.y;

            if(true)//if (hitY >= myY - climbRange && hitY <= myY + climbRange) // Ray에 맞은 이후, 범위 안에 있는지 체크
            {
                isGrab = true; // 그랩 상태
                myBody.useGravity = false; // 중력 해제
                myBody.velocity = Vector3.zero; // 벨로시티 값 조정
                grabStartY = transform.position.y; // 벽 잡은 순간 높이 저장

                Vector3 camForward = Camera.main.transform.forward; // 몸 방향 카메라 방향으로 맞추기
                camForward.y = 0f;
                if (camForward != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(camForward);
                }
            }
        }
    }
    void UnTryGrab()
    {
        if (isGrab) // 그랩상태이면
        {
            isGrab = false; // 그랩해제
            myBody.useGravity = true; // 중력 적용
        }
    }
    void Climbing()
    {
        {
            float verticalLook = Camera.main.transform.forward.y; // 바라보는 메인 카메라 y 축
            float currentY = transform.position.y; // 현재 카메라 y축

            float minY = grabStartY - climbRange;
            float maxY = grabStartY + climbRange;

            RaycastHit hit;
            Vector3 rayOrigin = grabPoint.position;
            Vector3 rayDirection = Camera.main.transform.forward; // 현재 벽에 여전히 붙어있는지 확인

            if (!Physics.Raycast(rayOrigin, rayDirection, out hit, grabRange, grabbableLayer)) // 벽에서 떨어지게 되면
            {
                UnTryGrab(); // 잡기 해제
                return;
            }

            if (verticalLook > 0.2f)
            {
                if (currentY > minY)
                {
                    Vector3 move = -Vector3.up * climbSpeed * Time.deltaTime; // 아래로 이동 (단, minY 이하로는 못 내려감)
                    if (transform.position.y + move.y < minY) // 이동하려는 위치가 minY보다 작으면 보정
                        move.y = minY - transform.position.y;

                    myBody.MovePosition(transform.position + move);
                }
            }
            else if (verticalLook < -0.2f)
            {
                if (currentY < maxY)
                {
                    Vector3 move = Vector3.up * climbSpeed * Time.deltaTime;// 위로 이동 (단, maxY 이상은 못 올라감)
                    if (transform.position.y + move.y > maxY)// 이동하려는 위치가 maxY보다 크면 보정
                        move.y = maxY - transform.position.y;

                    myBody.MovePosition(transform.position + move);
                }
            }
        }
    }

    void WallJump() // 벽 잡은 상태에서 점프 (위로 튕겨내기)
    {
        isGrab = false;
        myBody.useGravity = true;
        myBody.velocity = new Vector3(myBody.velocity.x, climbJumpForce, myBody.velocity.z);
        animator.SetBool("Jump", true);
        animator.SetBool("OnGround", false);
    }

    void DrawGrabRay() // 확인용 코드
    {
        RaycastHit hit;
        Vector3 rayOrigin = grabPoint.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, grabRange, grabbableLayer))
        {
            Debug.DrawRay(rayOrigin, rayDirection * grabRange, Color.green);
            Debug.Log("벽 감지됨!", this);
        }
        else
        {
            Debug.DrawRay(rayOrigin, rayDirection * grabRange, Color.red);
            Debug.Log("벽 없음...", this);
        }
    }

}