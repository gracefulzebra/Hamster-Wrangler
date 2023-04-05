using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField] private int blowerScore, mowerScore, lighterScore, zapperScore, rakeScore;
    public int comboScore;
   
    public void InitializeScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
    }
   
    void UpdateScoreDisplay()
    {  
        GetComponent<GameManager>().DisplayScore(currentScore);
    }

    public void UpdateScore(int blowerCount, int zapperCount, int lighterCount, int mowerCount, int rakeCount, int comboBonusScore)
    {
        currentScore += (blowerCount * blowerScore) + (zapperCount * zapperScore) + (lighterCount * lighterScore) + (mowerCount * mowerScore) + (rakeCount * rakeScore);
        currentScore += comboBonusScore;
    }

    public int FinalizeScore(int healthRemaining, int maxHealth)
    {
        float m_healthRemaining = healthRemaining;
        float m_maxHealth = maxHealth;

        float prcntHealth = m_healthRemaining / m_maxHealth;
        float tempScore = currentScore;

        tempScore *= prcntHealth;
        currentScore = (int)tempScore;
        
        return currentScore;
    }
    
}
