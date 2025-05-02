using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float fallMultiplier = 2.5f;
    private bool isGround;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround) // 땅에 붙어 있을 때 점프하게 구현
        {
            Jump();
        }

        else
        {
            animator.SetBool("Jump", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
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

            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;
            Vector3 targetVelocity = moveDir * moveSpeed;

            // 현재 속도를 목표 속도에 맞춰 부드럽게 이동
            Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            animator.SetFloat("Forward", horizontalVelocity.magnitude);
        }
        else
        {
            // 멈출 땐 수평 속도를 서서히 줄임
            Vector3 reducedVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, moveSpeed * Time.fixedDeltaTime);
            rb.velocity = new Vector3(reducedVelocity.x, velocity.y, reducedVelocity.z);
            animator.SetFloat("Forward", horizontalVelocity.magnitude);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("Jump", true);
        Debug.Log("isGround는 false");
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