using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] int grabRange; // 잡는 범위
    [SerializeField] int grabbableLayer; // 잡을 수 있는 레이어
    public KeyCode grabKey = KeyCode.E; // 잡는 키 상호작용

    private FixedJoint grabJoint; // 사슬? 고리? 같은 개념의 잡기
    private Rigidbody grabbedBody; // 지금 잡고 있는게 뭔지 변수로 지정



    void Update()
    {

        if (Input.GetKeyDown(grabKey)) // 그랩 키 입력 e 들어오면
        {
            if (grabJoint == null) // 잡고 있을 때
            {
                TryGrab();
            }
            else // 아닐 때
            {
                Release();
            }
        }
    }


    void TryGrab()
    {
        RaycastHit isObject; // 레이캐스트로 오브젝트 판별
        if (Physics.Raycast(transform.position, transform.forward, out isObject, grabRange, grabbableLayer))
        {
            if (isObject.rigidbody != null) // 오브젝트의 물리 작용이 있으면
            {
                grabbedBody = isObject.rigidbody; // 잡고 있는 변수에 잡을 수 있는 오브젝트라고 리지드 바디 설정

                grabJoint = gameObject.AddComponent<FixedJoint>(); // 잡기를 붙여줌.
                grabJoint.connectedBody = grabbedBody;
                grabJoint.breakForce = 500f;  // 힘을 너무 세게 주면 자동으로 끊어질 수도 있음
                grabJoint.breakTorque = 500f; // 토크가 너무 세면 자동으로 끊어짐
            }
        }
    }



    void Release()
    {
        if (grabJoint != null) // 잡고 있을 때 놓으면
        {
            Destroy(grabJoint); // 잡고 있던거 푼다.
            grabJoint = null; // null로 기본값 되돌려줌.
            grabbedBody = null; // 저장했던 정보 비활성화.
        }
    }


}
