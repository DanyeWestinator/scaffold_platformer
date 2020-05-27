using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueMaster : MonoBehaviour
{
    private static int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateScore()
    {
        score = ScoreScript.scoreValue;
    }
}
