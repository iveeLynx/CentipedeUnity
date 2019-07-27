using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChanger : MonoBehaviour
{

    public GameObject score;

    // Start is called before the first frame update
    void Start()
    {
        score.GetComponent<Text>().text = "Score: " + Player.score;
    }

}
