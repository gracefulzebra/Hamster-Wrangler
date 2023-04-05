using UnityEngine;

public class GnomeKing : MonoBehaviour
{
    [SerializeField] Animator animator;

    bool activateTrap;

    private void Start()
    {
    }

    private void Update()
    {
    //    if (level has ended || already used) return;

        RaycastHit hit;
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mousePos, out hit, Mathf.Infinity))
            {
                if (hit.transform.name == "Gnome King")
                {
                    animator.SetTrigger("Rotation");
                }
            }
        }
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
