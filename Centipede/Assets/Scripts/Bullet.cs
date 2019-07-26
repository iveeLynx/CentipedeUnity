using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    /*
     * Script for moving bullet
     */
    private float speed = 50.0f;

    private SpriteRenderer sprite;
    private Vector3 direction;
    private Vector2 screenPosition;

    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }

    void Update()
    {
        // Move towards the bullet after it creates
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        // Destroy bullet if it goes out of the screen
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Damage unit which it enters but not if it is player
        Unit unit = collider.GetComponent<Unit>();
        if (unit && !(unit is Player) && !(unit is Bonus))
        {
            unit.ReceiveDamage();
            Destroy(this.gameObject);
        }
    }
}
