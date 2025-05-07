using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MapIntroMove : MonoBehaviour
{
    [SerializeField] Transform targetPos1;
    [SerializeField] Transform targetPos2;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float maxRotate = 10f;

    private void Update()
    {
        if (transform.position.y < -maxRotate)
        {
            CameraMove1();
        }
        else
        {
            CameraMove2();
        }
    }

    void CameraMove1()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos1.position, moveSpeed * Time.deltaTime);
        transform.RotateAround(targetPos1.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }

    void CameraMove2()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos2.position, moveSpeed * Time.deltaTime);
    }
}
