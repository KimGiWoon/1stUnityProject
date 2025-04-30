using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isDead = false;
    [SerializeField] private Rigidbody rb;

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("플레이어 사망!");

        GameManager.Inst.OnPlayerDied(); // 게임매니저에 사망 알림
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water")) // Water 태그에 닿으면
        {
            Debug.Log("플레이어가 물에 닿았다! 익사!");
            GameManager.Inst.OnPlayerDied(); // 게임매니저에 사망 알림
        }

        if (other.CompareTag("Goal"))
        {
            Debug.Log("플레이어가 목표 지점에 도달!");
            GameManager.Inst.OnPlayerReachedGoal(); // GameManager에 알림
        }

        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("세이브 포인트에 도달!");
            GameManager.Inst.UpdateSavePoint(other.transform); // 위치 전달
        }

    }


    public void Respawn(Vector3 position)
    {
        Debug.Log("플레이어가 리스폰 했다!");

 
        transform.position = position; // 위치 초기화

        isDead = false; // 죽었던 판정 초기화
        rb.velocity = Vector3.zero;

        // 필요한 경우 애니메이션, 기타 상태도 리셋 가능
    }
}


