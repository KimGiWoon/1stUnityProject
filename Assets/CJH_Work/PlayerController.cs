using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody rb;
    private bool isGround;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGround) // ���� �پ� ���� �� �� �����ϰ� ����
        {
            Jump();
        }
    }

    void Move() // wasd ����Ű�� ���� ������ ����
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 moveVelocity = move * moveSpeed;
        Vector3 currentVelocity = rb.velocity;
        rb.velocity = new Vector3(moveVelocity.x, currentVelocity.y, moveVelocity.z);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) // ���� ������ ���� ��
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision) // ������ �������� ��
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }


}
