using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

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

    public void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
        {
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, "Environmental", 0);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetType() == typeof(SphereCollider))
        {
            if (col.CompareTag("Hamster"))
            {
                ItemInteract(col.gameObject);
                col.transform.GetComponent<HamsterBase>().Kill();
            }
        }     
    }
}
