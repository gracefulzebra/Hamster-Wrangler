using Unity.VisualScripting;
using UnityEngine;

public class GnomeKing : MonoBehaviour
{
    [SerializeField] Animator animator;

    bool canUseTrap;

    private void Start()
    {
        canUseTrap = true;
    }

    private void Update()
    {
        if (GameManager.instance.waveManager.waveCompleted)
        {
            canUseTrap = true;
        }
    }

    private void OnMouseDown()
    {
        if (!canUseTrap)
            return;
        animator.SetTrigger("Rotation");
        GetComponent<SphereCollider>().enabled = true;
        canUseTrap = false;
    }

    // this is called in animation
    public void TurnOffCollider()
    {
        GetComponent<SphereCollider>().enabled = false;
    }
   
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetType() == typeof(SphereCollider))
        {
            if (col.CompareTag("Hamster"))
            {
                col.transform.GetComponent<HamsterBase>().Kill();               
            }
        }     
    }
}
