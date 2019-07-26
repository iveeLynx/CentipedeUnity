using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{


    // Base class for player, bonuses, centipede and mushrooms
    public virtual void ReceiveDamage()
    {
        SoundManagerScript.Play("boom");
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {

    }
}
