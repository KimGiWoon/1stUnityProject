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
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
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

    private void FixedUpdate() // �� ���� ����� FixedUpdate��!
    {
        Move();
    }


    void Move() // wasd ����Ű�� ���� ������ ����w
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 front = new Vector3(moveX, 0f, moveZ).normalized; // �Է� ������ ���ͷ� �������. ī�޶� ���⿡ ���� �� �� ����.

        float moveAmount = new Vector2(moveX, moveZ).magnitude;
        animator.SetFloat("Forward", moveAmount);
        
        if (front != Vector3.zero) // �������� 0�� �ƴϸ�
        {
            Vector3 camForward = Camera.main.transform.forward; // ī�޶� �� ����
            Vector3 camRight = Camera.main.transform.right; // ī�޶� ������ ����

            camForward.y = 0f; // ī�޶� y�� ����
            camRight.y = 0f; // ī�޶� y�� ����
            camForward.Normalize(); // ���ʹ� �����ϰ� ���⸸
            camRight.Normalize(); // ���ʹ� �����ϰ� ���⸸

            Vector3 moveDir = camForward * front.z + camRight * front.x; // �Է°��� ī�޶� ���� �������� ��ȯ


            rb.AddForce(moveDir * moveSpeed, ForceMode.Force);
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

    /*private void OnCollisionExit(Collision collision) // ������ �������� ��
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
        }
    }*/
}