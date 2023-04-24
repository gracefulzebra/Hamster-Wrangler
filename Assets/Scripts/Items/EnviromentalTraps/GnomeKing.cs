using System.Linq;
using UnityEngine;

public class GnomeKing : EnvironmentalBase
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject redLight;

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
            redLight.SetActive(true);
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
        GameManager.instance.uiManager.ChangeCursorDefault();
    }

    GameObject gnomeKingAudioObject;
    private void OnMouseDown()
    {
        if (!canUseTrap || !fullCycle)
            return;
        animator.SetTrigger("Rotation");
        GetComponent<SphereCollider>().enabled = true;
        GameManager.instance.audioManager.GnomeKingAudio();
        gnomeKingAudioObject = GameManager.instance.audioManager.gnomeKingNoise.Last();
        redLight.SetActive(false);
        canUseTrap = false;
        fullCycle = false;
    }

    // this is called in animation
    public void TurnOffCollider()
    {
        if (counter != 0)
        {
            GameObject temp = Instantiate(comboDisplayPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
            temp.GetComponent<ComboDisplay>().SetComboText("GNOMEKILL X" + counter);
        }
        Destroy(gnomeKingAudioObject);
        fullCycle = true;
        GetComponent<SphereCollider>().enabled = false;
        counter = 0;
    }

    int counter;
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetType() == typeof(SphereCollider))
        {
            if (col.CompareTag("Hamster"))
            {
                counter++;
                AddScore();
                ItemInteract(col.gameObject);
                col.transform.GetComponent<HamsterBase>().Kill();
            }
        }     
    }
}
