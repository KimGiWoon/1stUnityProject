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

        GameManager.Inst.OnPlayerDied();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            GameManager.Inst.OnPlayerDied();
        }

        if (other.CompareTag("Goal"))
        {
            GameManager.Inst.OnPlayerReachedGoal();
        }

        /*if (other.CompareTag("SavePoint"))
        {
            GameManager.Inst.UpdateSavePoint(other.transform);
        }*/

    }


    public void Respawn(Vector3 position)
    {
        transform.position = position;

        isDead = false;
        rb.velocity = Vector3.zero;
    }
}


