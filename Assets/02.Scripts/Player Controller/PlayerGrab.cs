using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] LayerMask grabbableLayer;
    [SerializeField] float climbJumpForce = 2f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] float climbRange = 0.05f;
    [SerializeField] private Transform grabPoint;
    public KeyCode grabKey = KeyCode.E;
    public KeyCode jumpKey = KeyCode.Space;
    private float grabStartY;
    private GameObject grabCandidate = null;
    public bool IsGrabbing => isGrab;
    private Animator animator;
    private bool isGrab = false;
    private Rigidbody myBody;
    private GameObject grabbedKey = null;

    public bool IsWallGrabbing => isGrab && grabbedKey == null;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 키를 잡고 있을 때 부드럽게 따라오게 설정
        if (grabbedKey != null)
        {
            Vector3 targetPos = grabPoint.position + grabPoint.forward * 0.1f;
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
            UnTryGrab();
        }


        void GrabKey(GameObject keyObj)
        {
            isGrab = true;
            grabbedKey = keyObj;

            Rigidbody keyRb = grabbedKey.GetComponent<Rigidbody>();
            if (keyRb != null)
            {
                //  잡을 때 물리 초기화
                keyRb.useGravity = false;
                keyRb.isKinematic = true;
                keyRb.velocity = Vector3.zero;
                keyRb.angularVelocity = Vector3.zero;
                keyRb.interpolation = RigidbodyInterpolation.Interpolate;
            }

            // 🌟 부모 설정
            grabbedKey.transform.SetParent(grabPoint);
            grabbedKey.transform.localPosition = new Vector3(0, 0, 0.1f);
            grabbedKey.transform.localRotation = Quaternion.identity;

            Debug.Log("키를 잡았습니다!");
        }

        void UnTryGrab()
        {
            if (!isGrab) return;

            if (grabbedKey != null)
            {
                Rigidbody keyRb = grabbedKey.GetComponent<Rigidbody>();

                //  물리 속도와 충돌 초기화
                if (keyRb != null)
                {
                    keyRb.velocity = Vector3.zero;
                    keyRb.angularVelocity = Vector3.zero;
                    keyRb.isKinematic = false;
                    keyRb.useGravity = true;
                    keyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }

                //  부모 해제 전에 위치 고정
                grabbedKey.transform.SetParent(null);

                //  충돌 안정화
                StartCoroutine(ResetGravityAfterFrame(keyRb));

                Debug.Log("키를 놓았습니다!");
            }

            isGrab = false;
            grabbedKey = null;
        }
    }
    

    private IEnumerator ResetGravityAfterFrame(Rigidbody keyRb)
    {
        yield return null;
        if (keyRb != null)
        {
            keyRb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (grabbedKey != null) return;

        if (other.CompareTag("Key"))
        {
            grabCandidate = other.gameObject;
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