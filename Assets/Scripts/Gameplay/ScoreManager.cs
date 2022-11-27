using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore = 0;

    [SerializeField] private int blowerScore, mowerScore, lighterScore, tarScore, rakeScore;

    void UpdateScoreDisplay()
    {
        GetComponent<GameManager>().DisplayScore(currentScore);
    }

    public void UpdateScore(int blowerCount, int tarCount, int lighterCount, int mowerCount, int rakeCount)
    {
        currentScore += (blowerCount * blowerScore) + (tarCount * tarScore) + (lighterCount * lighterScore) + (mowerCount * mowerScore) + (rakeCount * rakeScore); 
        UpdateScoreDisplay();
    }
    
}
