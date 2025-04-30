using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLSW : MonoBehaviour
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


    private void FixedUpdate() // 占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙占?FixedUpdate占쏙옙!

    

    {
        Move();
    }



    void Move() // wasd 占쏙옙占쏙옙키占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙

    

    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 front = new Vector3(moveX, 0f, moveZ).normalized; // 占쌉뤄옙 占쏙옙占쏙옙占쏙옙 占쏙옙占싶뤄옙 占쏙옙占쏙옙占쏙옙占? 카占쌨띰옙 占쏙옙占썩에 占쏙옙占쏙옙 占쏙옙 占쏙옙 占쏙옙占쏙옙.

        if (front != Vector3.zero) // 占쏙옙占쏙옙占쏙옙占쏙옙 0占쏙옙 占싣니몌옙
        {
            Vector3 camForward = Camera.main.transform.forward; // 카占쌨띰옙 占쏙옙 占쏙옙占쏙옙
            Vector3 camRight = Camera.main.transform.right; // 카占쌨띰옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙

            camForward.y = 0f; // 카占쌨띰옙 y占쏙옙 占쏙옙占쏙옙
            camRight.y = 0f; // 카占쌨띰옙 y占쏙옙 占쏙옙占쏙옙
            camForward.Normalize(); // 占쏙옙占싶댐옙 占쏙옙占쏙옙占싹곤옙 占쏙옙占썩만
            camRight.Normalize(); // 占쏙옙占싶댐옙 占쏙옙占쏙옙占싹곤옙 占쏙옙占썩만

            Vector3 moveDir = camForward * front.z + camRight * front.x; // 占쌉력곤옙占쏙옙 카占쌨띰옙 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙占쏙옙 占쏙옙환


            rb.AddForce(moveDir * moveSpeed, ForceMode.VelocityChange);
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



    private void OnCollisionEnter(Collision collision) // ���� ������ ���� ��
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("isGround는 true");
            isGround = true;
            animator.SetBool("OnGround", true);
        }
    }



}


