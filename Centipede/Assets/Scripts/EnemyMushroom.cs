using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Unit
{

    // Mushroom;

    private int lives = 3;
    Bonus AdditionalLives;

    public override void ReceiveDamage()
    {
        lives--;

        if (lives <= 0)
        {
            // Kind of random bonus generator with chance
            int chance = Random.Range(0, 100);
            if (chance>= 85 && chance <=94)
            {
                // Spawn life bonus
                AdditionalLives = Resources.Load<Bonus>("LifeBonus");
                // Implement bonus appearance with chance
                Bonus newBonus = Instantiate(AdditionalLives, transform.position, AdditionalLives.transform.rotation);
                Bonus.bonusNumber = 0;
            } 
            else if (chance >= 95)
            {
                // Spawn damage speed boost
                AdditionalLives = Resources.Load<Bonus>("DoubleShot");
                // Implement bonus appearance with chance
                Bonus newBonus = Instantiate(AdditionalLives, transform.position, AdditionalLives.transform.rotation);
                Bonus.bonusNumber = 1;
            }
            Player.score += (Player.wave * 25);
            base.ReceiveDamage();
        }
    }

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
        if (unit is Player)
        {
            unit.ReceiveDamage();
        }
        if(unit is EnemyMushroom)
        {
            EnemyMushroom EnterMush = new EnemyMushroom();
            //EnterMush.
            Vector3 pos = transform.position;
            pos.x += 0.5f;
            pos.y += 0.5f;
            transform.position = pos;
        }
    }

}
