using UnityEngine;

public class CheeseHealth : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<HamsterBase>() != null)
        {
            col.GetComponent<HamsterBase>().Despawn();
            gameManager.LoseHealth();
        }
        
    }
}