using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySegment : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        Debug.Log("ENTERED");
        if (unit is Player)
        {
            Debug.Log("DAMAGED");
            unit.ReceiveDamage();
        }
        //if (collider.GetComponent<Bullet>())
        //{
        //    Debug.Log("SSS");
        //    this.ReceiveDamage();
        //}
    }
}
