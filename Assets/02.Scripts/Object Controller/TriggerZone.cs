using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject pencilObject; // 씬에 미리 배치된 연필 오브젝트
    private bool isActivated = false;

    private void Awake()
    {
        // 🌟 시작할 때 연필을 비활성화하여 스폰 포인트로 사용
        if (pencilObject != null)
        {
            pencilObject.SetActive(false);
            Debug.Log("Awake: 연필이 비활성화되었습니다!");
        }
        else
        {
            Debug.LogWarning("Awake: 연필 오브젝트가 할당되지 않았습니다!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 이미 활성화된 경우 중복 방지
        if (isActivated) return;

        // Key 태그를 가진 오브젝트가 들어왔을 때만 실행
        if (other.CompareTag("Key"))
        {
            //  연필 오브젝트를 활성화
            if (pencilObject != null)
            {
                pencilObject.SetActive(true); // 기존 위치에서 활성화
                isActivated = true; // 한 번만 활성화되도록 설정
                Debug.Log("연필이 활성화되었습니다!");

                Destroy(other.gameObject);
            }
            else
            {
                Debug.LogWarning("연필 오브젝트가 할당되지 않았습니다!");
            }
        }
    }
}