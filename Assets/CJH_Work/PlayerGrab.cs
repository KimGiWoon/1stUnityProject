using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] int grabRange; // ��� ����
    [SerializeField] LayerMask grabbableLayer; // ���� �� �ִ� ���̾�
    [SerializeField] float climbJumpForce = 2f; // ������ ��
    [SerializeField] float climbSpeed = 2f; // Ŭ���̹� ���ǵ�
    public KeyCode grabKey = KeyCode.E; // ��� Ű ��ȣ�ۿ�
    public KeyCode jumpKey = KeyCode.Space; // ���� Ű ��ȣ�ۿ�
    private float grabStartY; // �� ���� ���� �÷��̾� ���� ����
    [SerializeField] float climbRange = 0.05f; // Ŭ���̹� ����


    private bool isGrab = false;
    private Rigidbody myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Transform cam = Camera.main.transform;
        Vector3 rayOrigin = cam.position;
        Vector3 rayDirection = cam.forward;
        DrawGrabRay(); // Ȯ�ο� ���� �׸���

        if (Input.GetKeyDown(grabKey)) // Ű �Է� ���� ��
        {
            TryGrab();
        }

        if (Input.GetKeyUp(grabKey)) // Ű �Է��� ����
        {
            UnTryGrab();
        }


        if (isGrab) // �׷� ���� �� ��
        {
            Climbing();

            // �� ���� ���¿����� ����
            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
    }

    void TryGrab()
    {
        RaycastHit isObject; // ����ĳ��Ʈ�� ������Ʈ �Ǻ�
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out isObject, grabRange, grabbableLayer)) 
        {
            float hitY = isObject.point.y;
            float myY = transform.position.y;

            
            if (hitY >= myY - climbRange && hitY <= myY + climbRange) // Ray�� ���� ����, ���� �ȿ� �ִ��� üũ
            {
                isGrab = true; // �׷� ����
                myBody.useGravity = false; // �߷� ����
                myBody.velocity = Vector3.zero; // ���ν�Ƽ �� ����
                grabStartY = transform.position.y; // �� ���� ���� ���� ����

                Vector3 camForward = Camera.main.transform.forward; // �� ���� ī�޶� �������� ���߱�
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
        if (isGrab) // �׷������̸�
        {
            isGrab = false; // �׷�����
            myBody.useGravity = true; // �߷� ����
        }
    }

    void Climbing()
    {
        {
            float verticalLook = Camera.main.transform.forward.y; // �ٶ󺸴� ���� ī�޶� y ��
            float currentY = transform.position.y; // ���� ī�޶� y��

            float minY = grabStartY - climbRange;
            float maxY = grabStartY + climbRange;

            RaycastHit hit;
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = Camera.main.transform.forward; // ���� ���� ������ �پ��ִ��� Ȯ��

            if (!Physics.Raycast(rayOrigin, rayDirection, out hit, grabRange, grabbableLayer)) // ������ �������� �Ǹ�
            {
                UnTryGrab(); // ��� ����
                return;
            }

            if (verticalLook > 0.2f)
            {
                if (currentY > minY)
                {
                    Vector3 move = -Vector3.up * climbSpeed * Time.deltaTime; // �Ʒ��� �̵� (��, minY ���Ϸδ� �� ������)
                    if (transform.position.y + move.y < minY) // �̵��Ϸ��� ��ġ�� minY���� ������ ����
                        move.y = minY - transform.position.y;

                    myBody.MovePosition(transform.position + move);
                }
            }
            else if (verticalLook < -0.2f)
            {
                if (currentY < maxY)
                {
                    Vector3 move = Vector3.up * climbSpeed * Time.deltaTime;// ���� �̵� (��, maxY �̻��� �� �ö�)
                    if (transform.position.y + move.y > maxY)// �̵��Ϸ��� ��ġ�� maxY���� ũ�� ����
                        move.y = maxY - transform.position.y;

                    myBody.MovePosition(transform.position + move);
                }
            }
        }
    }

        void WallJump() // �� ���� ���¿��� ���� (���� ƨ�ܳ���)
    { 
            isGrab = false;
            myBody.useGravity = true;
            myBody.velocity = new Vector3(myBody.velocity.x, climbJumpForce, myBody.velocity.z);
        }

        void DrawGrabRay() // Ȯ�ο� �ڵ�
        {
            RaycastHit hit;
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = Camera.main.transform.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, grabRange, grabbableLayer))
            {
                Debug.DrawRay(rayOrigin, rayDirection * grabRange, Color.green);
            }
            else
            {
                Debug.DrawRay(rayOrigin, rayDirection * grabRange, Color.red);
            }
        }
    
}
