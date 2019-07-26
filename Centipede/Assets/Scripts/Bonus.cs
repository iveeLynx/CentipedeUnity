using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Unit
{

    /*
     * Script for bonuses
     */

    private float minX, maxX, minY, maxY;
    public static int bonusNumber = 0;

    // If it reaches player - gives bonus
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit is Player)
        {
            if (bonusNumber == 0)
            {
                Player.lives += 5;
                base.ReceiveDamage();
            }
            else if (bonusNumber == 1)
            {
                if(Player.shotCooldown >= 0.1f) Player.shotCooldown -= 0.01f; 
                base.ReceiveDamage();
            }
        }
        else if (unit)
        {

        }
    }


    private void GetCameraSizes()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;
    }
}
