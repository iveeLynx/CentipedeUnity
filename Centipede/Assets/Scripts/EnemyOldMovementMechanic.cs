using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOldMovementMechanic : Unit
{

    /*
     * WARNING: NOT IN USE
     * 
     * It was an old mechanic for moving centipede
     * It was providing Grid-like moving.
     */



    [SerializeField]
    private float speed = 20.0f;

    private int wave = 5;

    EnemyMushroom enemyMushroom;

    private SpriteRenderer spriteRenderer;
    private Sprite headSprite;

    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;

    private Vector3 position;
    private Vector3 direction;

    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private float spawnTimer;

    public static bool isFirst = true;

    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        enemyMushroom = Resources.Load<EnemyMushroom>("MushObstacle");

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isFirst)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("EnemyHead2");
        }
        else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("EnemyHead4");
        }

        GetCameraSizes();

        gridPosition = new Vector2Int(0, (int)maxY - 1);
        gridMoveTimerMax = 0.1f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
    }

    void Start()
    {

    }

    void Update()
    {
        //Vector3 pos = transform.position;
        //pos.x += Input.GetAxis(directionkey) * speed * Time.deltaTime;
        //if (pos.x <= minX) pos.x = minX;
        //else if (pos.x >= maxX) pos.x = maxX;
        //transform.position = pos;
        //if (true)
        //{

        //}

        spriteRenderer = GetComponent<SpriteRenderer>();

        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            if (gridPosition.x >= maxX)
            {
                //gridMoveDirection = new Vector2Int(0, -1);
                gridPosition.y -= 2;
                gridPosition.x -= 1;
                gridMoveDirection = new Vector2Int(-1, 0);
                spriteRenderer.flipX = true;
                transform.position = new Vector3(gridPosition.x, gridPosition.y);
            }
            else if (gridPosition.x <= minX)
            {
                //gridMoveDirection = new Vector2Int(0, -1);
                gridPosition.y -= 2;
                gridPosition.x += 1;
                gridMoveDirection = new Vector2Int(1, 0);
                spriteRenderer.flipX = false;
                transform.position = new Vector3(gridPosition.x, gridPosition.y);
            }
            else if (gridPosition.y <= minY)
            {
                Player.livingSegments--;
                Destroy(gameObject);
            }
            else
            {
                gridPosition += gridMoveDirection;
            }
            gridMoveTimer -= gridMoveTimerMax;
        }

        transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit is Player)
        {
            unit.ReceiveDamage();
        }
        if (unit is EnemyMushroom)
        {
            //unit.ReceiveDamage();
            gridMoveDirection = new Vector2Int(gridMoveDirection.x * -1, gridMoveDirection.y);
            if (gridMoveDirection.x == -1) spriteRenderer.flipX = true;
            if (gridMoveDirection.x == 1) spriteRenderer.flipX = false;
            gridPosition.y -= 2;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
    }

    public override void ReceiveDamage()
    {
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
