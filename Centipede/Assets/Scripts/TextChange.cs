using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{

    
    public GameObject score;

    void Start()
    {

    }

    void Update()
    {
        SetTexts();
    }

    // Update score, wave and lives
    public void SetTexts()
    {
        score.GetComponent<Text>().text = "Score: " + Player.score + "\nLives: " + Player.lives + "\nWave: " + Player.wave;
    }
}
