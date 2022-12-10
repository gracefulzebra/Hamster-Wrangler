using UnityEngine;

public class CheeseHealth : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider col)
    {     
        gameManager.health -= 10;
        print(gameManager.health); 
        gameManager.CheckIfLoseGame();
    }
}