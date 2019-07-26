using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Unit
{

    /* 
     * Script for player's ship
     */

    [SerializeField]
    public static int lives = 10;
    [SerializeField]
    private float speed = 20.0f;

    public static int wave = 1;
    private float cooldown = 0;
    private float invulnerable = 0;

    public static float shotCooldown = 0.25f;

    public bool isAlive = true;
    public static int score = 0;

    private Bullet bullet;

    private BoxCollider2D boxCollider2D;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRender;
    private Vector2 screenPosition;

    private float timer;
    private float timerMax;
    private int segments = 0;
    public static int livingSegments = 0;

    private float leftradius;
    private float upperradius;

    private float minX, maxX, minY, maxY;

    // Variables for loading prefabs
    EnemyHead enemyHead;
    EnemyMushroom enemyMushroom;

    Camera cam;
    float cameraHeigth;
    float cameraWidth;

    // Variables for blinking
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 1.0f;
    public bool startBlinking = false;



    private void Awake()
    {
        // Create class object for creating arraylist with Cintepede segments info
        SegmentInfo segInfo = new SegmentInfo();


        // Timer for generating segments
        timerMax = 0.8f;
        timer = timerMax;
        spriteRender = GetComponent<SpriteRenderer>();

        // Find length from center of player's ship to border
        boxCollider2D = GetComponent<BoxCollider2D>();
        Vector3 pos = transform.position;
        leftradius = pos.x - boxCollider2D.bounds.min.x;
        upperradius = pos.y - boxCollider2D.bounds.min.y;

        // Loading prefabs
        enemyMushroom = Resources.Load<EnemyMushroom>("MushObstacle");
        enemyHead = Resources.Load<EnemyHead>("EnemyHead");
        bullet = Resources.Load<Bullet>("Bullet");
    }



    void Start()
    {
        // Preparing game rules
        score = 0;
        lives = 10;
        segments = wave * 2 + 2;
        wave = 0;
        livingSegments = 0;

        isAlive = true;

        GetCameraSizes();

        GenerateMushrooms();

    }

    void Update()
    {
        // Update cooldowns
        if (timer != 0) timer -= Time.deltaTime;
        cooldown -= Time.deltaTime;
        invulnerable -= Time.deltaTime;

        // Check if centipede is alive. If alive and has no segments - create again but with more segments
        if (isAlive && livingSegments <= 0)
        {
            isAlive = false;
            EnemyHead.isFirst = true;
            segments = wave * 2 + 2;
            livingSegments = segments;
            EnemyHead.speed += 1.0f;
            wave++;
        }
        else if (timer <= 0 && segments > 0)
        {
            CreateCentipede();
            EnemyHead.isFirst = false;
        }
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Check if button pressed, if pressed - move player or shoot

        if (Input.GetButton("Horizontal"))
        {
            Movement("Horizontal");
        }
        if (Input.GetButton("Vertical"))
        {
            Movement("Vertical");
        }
        if (Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetButton("Fire1") && cooldown <= 0) Shoot();

        // Start blinking player sprite
        if (startBlinking == true)
        {
            SpriteBlinkingEffect();
        }
    }

    /*  Moves player. 
     *  directionkey - show which key pressed(horizontal move key or vertical)
    */
    private void Movement(string directionkey)
    {
        Vector3 pos = transform.position;
        switch (directionkey)
        {
            case "Horizontal":
                pos.x += Input.GetAxis(directionkey) * speed * Time.deltaTime;
                // Check if player is on the border of the screen. If yes - don't let him go out of screen
                // If player is on the left
                if (pos.x <= minX + leftradius) pos.x = minX + leftradius;
                // If player is on the right
                else if (pos.x >= maxX - leftradius) pos.x = maxX - leftradius;
                transform.position = pos;
                break;
            case "Vertical":
                pos.y += Input.GetAxis(directionkey) * speed * Time.deltaTime;
                if (pos.y <= minY + upperradius) pos.y = minY + upperradius;
                // If player is on the bottom
                else if (pos.y >= maxY - upperradius) pos.y = maxY - upperradius;
                // If player is on the top
                transform.position = pos;
                break;

        }
    }

    // Override ReceiveDamage from Unit class
    public override void ReceiveDamage()
    {
        // If player isn't invulnerable - receive damage and lose live
        if (invulnerable <= 0)
        {
            startBlinking = true;
            lives--;
            invulnerable = 1f;
        }
    }

    // Perfoms shooting
    private void Shoot()
    {

        Vector3 position = transform.position;
        position.y += 1.0F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);

        newBullet.Direction = newBullet.transform.up;

        // PLays sound
        SoundManagerScript.Play("shot");

        // Set cooldown
        cooldown = shotCooldown;
    }

    // Get Maximum and minimum points of screen
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

    // Creates segment of centipede
    private void CreateCentipede()
    {
        Vector3 position = transform.position;
        position.x = 0;
        position.y = maxY - 1;
        EnemyHead newEnemyHead = Instantiate(enemyHead, position, enemyHead.transform.rotation);
        SegmentInfo.centipede.Add(newEnemyHead);
        segments--;
        timer = 0.2f;
        if (segments == 0) isAlive = true;
    }


    // Blink player's ship sprite if it damaged
    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    // Random generating mushrooms
    private void GenerateMushrooms()
    {
        for (int i = (int)minX + 2; i < (int)maxX - 2; i++)
        {
            for (int j = (int)maxY - 10; j > (int)minY + 10; j--)
            {
                int chance = Random.Range(0, 100);
                Debug.Log(chance);
                if (chance >= 97)
                {
                    EnemyMushroom newEnemyMushroom = Instantiate(enemyMushroom, new Vector3(i, j), enemyMushroom.transform.rotation);
                }
            }
        }
        Debug.Log((int)minY + "     " + (int)maxY);

    }

}
