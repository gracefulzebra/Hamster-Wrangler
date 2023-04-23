using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class GnomeKing : EnvironmentalBase
{
    [SerializeField] Animator animator;

    bool canUseTrap;
    bool fullCycle;

    private void Start()
    {
        canUseTrap = true;
        fullCycle = true;
    }

    private void Update()
    {
        if (GameManager.instance.waveManager.waveCompleted)
        {
            canUseTrap = true;
        }
    }

    void OnMouseOver()
    {
        if (GameManager.instance.uiManager.defaultCursor == null)
            return;
        if (!GameManager.instance.currencyManager.deleteItemMode)
        {
            GameManager.instance.uiManager.overTrap = true;
        }
    }
    void OnMouseExit()
    {
        if (GameManager.instance.uiManager.defaultCursor == null)
            return;
        GameManager.instance.uiManager.overTrap = false;
        GameManager.instance.uiManager.ChangeCursor();
    }

    private void OnMouseDown()
    {
        if (!canUseTrap || !fullCycle)
            return;
        animator.SetTrigger("Rotation");
        GetComponent<SphereCollider>().enabled = true;
        GameObject temp = Instantiate(comboDisplayPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
        GameManager.instance.audioManager.GnomeKingAudio();
        temp.GetComponent<ComboDisplay>().SetComboText("GNOMEKILL");
        canUseTrap = false;
        fullCycle = false;
    }

    // this is called in animation
    public void TurnOffCollider()
    {
        fullCycle = true;
        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetType() == typeof(SphereCollider))
        {
            if (col.CompareTag("Hamster"))
            {
                AddScore();
                ItemInteract(col.gameObject);
                col.transform.GetComponent<HamsterBase>().Kill();
            }
        }     
    }
}
