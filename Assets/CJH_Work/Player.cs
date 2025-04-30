using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{       
    public int life = 3;
        public void TakeDamage(int dead)
        {
            life -= dead;
        }
}
