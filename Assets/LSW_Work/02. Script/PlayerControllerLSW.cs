using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLSW : MonoBehaviour
{
    [SerializeField] private PhysicMaterial lowFrictionMaterial;
    [SerializeField] private PhysicMaterial highFrictionMaterial;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float fallMultiplier;
    [SerializeField] private float maxSpeed;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Collider _collider;
    private Vector3 _horizontalVelocity;
    private bool _isGround;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround) // 땅에 붙어 있을 때 점프하게 구현
        {
            Jump();
        }
        else
        {
            _animator.SetBool("Jump", false);
        }

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.AddForce(Vector3.up * (Physics.gravity.y * (fallMultiplier - 1)), ForceMode.Acceleration);
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

        Vector3 inputDir = new Vector3(moveX, 0f, moveZ).normalized;

        if (inputDir != Vector3.zero)
        {
            _collider.material = lowFrictionMaterial;
            
            Vector3 camForward = Camera.main.transform.forward; // 카메라 기준 방향 계산
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x; // 입력값을 카메라 기준 방향에 맞춰 변환
            // 현재 속도의 수평 성분
            _horizontalVelocity = _rigidbody.velocity;
            _horizontalVelocity.y = 0f;
            _animator.SetFloat("Forward", _horizontalVelocity.magnitude);
            // 반대 방향 감속 보정
            float dot = Vector3.Dot(_horizontalVelocity.normalized, moveDir);
            
            if (dot < 0f)
            {
                // 반대 방향 입력이면 기존 속도 줄이기
                _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, new Vector3(0, _rigidbody.velocity.y, 0), Time.fixedDeltaTime * 5f);
            }
            
            if (_rigidbody.velocity.magnitude < maxSpeed)
            {
                _rigidbody.AddForce(moveDir * moveSpeed, ForceMode.VelocityChange);
            }

            Quaternion targetRotation = Quaternion.LookRotation(moveDir); // 회전 처리
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            _collider.material = highFrictionMaterial;
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
            
            _horizontalVelocity = _rigidbody.velocity;
            _horizontalVelocity.y = 0f;
            _animator.SetFloat("Forward", _horizontalVelocity.magnitude);
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _animator.SetBool("Jump", true);
        _isGround = false;
        _animator.SetBool("OnGround", false);
    }
    
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
            _animator.SetBool("OnGround", true);
        }
    }
}


