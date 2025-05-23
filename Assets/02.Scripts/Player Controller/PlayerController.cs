using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float deceleration = 15f;
    private bool isGround;
    private Animator animator;
    private PlayerGrab grab;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        grab = GetComponent<PlayerGrab>(); // 같은 오브젝트에 붙어 있을 경우
    }


    void Update()
    {
        // 열쇠를 잡고 있어도 점프는 가능해야 하므로, WallGrabbing일 때만 막는다
        if (grab != null && grab.IsWallGrabbing) return; // 벽을 잡고 있을 때만 점프 차단
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                Jump();
            }
            else
            {
                animator.SetBool("Jump", false);
            }

            // 중력 보정
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (grab != null && grab.IsWallGrabbing) return; // ❌ 그랩 중일 땐 이동 차단
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 velocity = rb.velocity;
        Vector3 inputDir = new Vector3(moveX, 0f, moveZ).normalized;
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);

        if (inputDir != Vector3.zero)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = (camForward * inputDir.z + camRight * inputDir.x).normalized;
            Vector3 targetVelocity = moveDir * moveSpeed;

            // ✅ 가속 적용
            Vector3 newVelocity = Vector3.MoveTowards(horizontalVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector3(newVelocity.x, velocity.y, newVelocity.z);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else
        {
            // ✅ 감속 적용
            Vector3 reducedVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector3(reducedVelocity.x, velocity.y, reducedVelocity.z);
        }

        // ✅ 현재 속도 반영해서 애니메이션 적용
        Vector3 currentHorizontal = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        animator.SetFloat("Forward", currentHorizontal.magnitude);
    }

    void Jump()
    {
        Vector3 currentVelocity = rb.velocity;
        currentVelocity.y = 0f; // 기존 y속도를 제거하여 중첩 방지
        rb.velocity = currentVelocity + Vector3.up * jumpForce;

        animator.SetBool("Jump", true);
        isGround = false;
        animator.SetBool("OnGround", false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("isGround는 true");
            isGround = true;
            animator.SetBool("OnGround", true);
        }
    }

}