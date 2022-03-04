using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text text;
    public PlayerMovement score;

    void Start()
    {
        
    }

    void Update()
    {
        text.text = "Distance: " + ((int)score.distance + " Coins: " + (score.coins) + " Lives: " + (score.lives) + " Score: " + (int)score.distance * (score.coins * 100));
    }
}
