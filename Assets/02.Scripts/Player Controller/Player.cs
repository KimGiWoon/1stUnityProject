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
        Debug.Log("�÷��̾� ���!");

        GameManager.Inst.OnPlayerDied(); // ���ӸŴ����� ��� �˸�
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water")) // Water �±׿� ������
        {
            Debug.Log("�÷��̾ ���� ��Ҵ�! �ͻ�!");
            GameManager.Inst.OnPlayerDied(); // ���ӸŴ����� ��� �˸�
        }

        if (other.CompareTag("Goal"))
        {
            Debug.Log("�÷��̾ ��ǥ ������ ����!");
            GameManager.Inst.OnPlayerReachedGoal(); // GameManager�� �˸�
        }

        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("���̺� ����Ʈ�� ����!");
            GameManager.Inst.UpdateSavePoint(other.transform); // ��ġ ����
        }

    }


    public void Respawn(Vector3 position)
    {
        Debug.Log("�÷��̾ ������ �ߴ�!");

 
        transform.position = position; // ��ġ �ʱ�ȭ

        isDead = false; // �׾��� ���� �ʱ�ȭ
        rb.velocity = Vector3.zero;

        // �ʿ��� ��� �ִϸ��̼�, ��Ÿ ���µ� ���� ����
    }
}


