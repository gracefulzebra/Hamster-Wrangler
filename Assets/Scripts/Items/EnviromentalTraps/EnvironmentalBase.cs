using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalBase : MonoBehaviour
{

    [SerializeField] protected int scorePerKill;
    [SerializeField] protected GameObject comboDisplayPrefab;

    public void AddScore()
    {
       GameManager.instance.scoreManager.currentScore += scorePerKill;
    }

    public void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
        {
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, "Environmental", 0);
        }
    }

}
