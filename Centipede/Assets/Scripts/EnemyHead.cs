using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : Unit
{


    [SerializeField]
    public static float speed = 60.0f;

    private ArrayList centipede;
    private int segmentsCount = 0;
    private int segmentNumber = 0;

    private int wave = 5;

    EnemyMushroom enemyMushroom;

    public SpriteRenderer spriteRenderer;
    private Sprite headSprite;

    private Vector3 position;
    private Vector3 direction;

    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private float spawnTimer;

    public static bool isFirst = true;

    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        // Load prefab
        enemyMushroom = Resources.Load<EnemyMushroom>("MushObstacle");

        // Simulating segment ID
        segmentNumber = SegmentInfo.AddSegment();

        spriteRenderer = GetComponent<SpriteRenderer>();

        // If it's a first segment in centipede - make it main(head)
        if (isFirst)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("EnemyHead2");
        }
        else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("EnemyHead4");
        }

        GetCameraSizes();

        position = new Vector3(0, maxY - 1);
        direction = new Vector3(0.1f, 0);
    }

    void Update()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        gridMoveTimer += Time.deltaTime;

        Movement();

    }

    // Moves centipede segments 
    private void Movement()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector3 pos = transform.position;


        // If segment reachs border - flip it and reverse direction
        if (position.x >= maxX)
        {
            position.y -= 0.7f;
            position.x -= 0.1f;
            direction = new Vector3(-0.1f, 0);
            spriteRenderer.flipX = true;
            transform.position = new Vector3(position.x, position.y);
        }
        else if (position.x <= minX)
        {
            //gridMoveDirection = new Vector2Int(0, -1);
            position.y -= 0.7f;
            position.x += 0.1f;
            direction = new Vector3(0.1f, 0);
            spriteRenderer.flipX = false;
            transform.position = new Vector3(position.x, position.y);
        }
        //If it reachs bottom of the screen - destroy and damage player
        else if (position.y <= minY)
        {
            Player.livingSegments--;
            Destroy(gameObject);
        }
        else
        {
            position += direction * speed * Time.deltaTime;
        }
        transform.position = position;
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Damage player when it bumps into it
        Unit unit = collider.GetComponent<Unit>();
        if (unit is Player)
        {
            unit.ReceiveDamage();
        }
        // If it bumps into mushroom - reverse direction
        if (unit is EnemyMushroom)
        {
            direction = new Vector3(direction.x * -1, direction.y);
            if (direction.x < 0) spriteRenderer.flipX = true;
            if (direction.x > 0) spriteRenderer.flipX = false;
            position.y -= 0.7f;
            transform.position = new Vector3(position.x, position.y);
        }
       
    }

    public override void ReceiveDamage()
    {
        try
        {
            if (SegmentInfo.centipede[segmentNumber + 1] != null)
            {
                EnemyHead CurrentSegment = (EnemyHead)SegmentInfo.centipede[segmentNumber + 1];
                CurrentSegment.spriteRenderer.sprite = Resources.Load<Sprite>("EnemyHead2");
            }

        }
        catch (Exception exception)
        {
            Debug.Log(exception.ToString());
        }
        
        Vector3 position = transform.position;
        EnemyMushroom newEnemyMushroom = Instantiate(enemyMushroom, position, enemyMushroom.transform.rotation);
        Player.score += (Player.wave * 5);
        Player.livingSegments--;
        base.ReceiveDamage();
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
