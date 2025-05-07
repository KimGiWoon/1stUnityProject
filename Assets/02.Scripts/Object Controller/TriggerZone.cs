using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject objectToActivate; // 상호작용할 오브젝트 연필

    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated) return;
        {
            if (other.CompareTag("Key")) // Key 오브젝트는 "Key" 태그를 갖고 있어야 함
            {

                if (objectToActivate != null)
                {
                    objectToActivate.SetActive(true); // 연필 활성화
                }

                if (objectToActivate != null)
                {

                    isActivated = true; // 중복 방지
                }
            }
        }
    }
}
