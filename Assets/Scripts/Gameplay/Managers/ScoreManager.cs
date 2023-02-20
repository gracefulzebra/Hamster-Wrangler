using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;

    [SerializeField] private int blowerScore, mowerScore, lighterScore, tarScore, rakeScore;

    public void InitializeScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {  
        GetComponent<GameManager>().DisplayScore(currentScore);
    }

    public void UpdateScore(int blowerCount, int tarCount, int lighterCount, int mowerCount, int rakeCount)
    {
        currentScore += (blowerCount * blowerScore) + (tarCount * tarScore) + (lighterCount * lighterScore) + (mowerCount * mowerScore) + (rakeCount * rakeScore); 
    }

    public int FinalizeScore(int healthRemaining, int maxHealth)
    {
        float prcntHealth = (float)healthRemaining / (float)maxHealth;
        float tempScore = currentScore;
        
        tempScore *= prcntHealth;
        
        currentScore = (int)tempScore;

        return currentScore;
    }
    
}
