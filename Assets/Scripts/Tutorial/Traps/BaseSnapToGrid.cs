using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseSnapToGrid : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler
{
    protected GameObject gridRefObject;
    protected GridGenerator gridRef;
    public bool hasItem;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);
    [SerializeField] ParticleSystem placementEffect;
    public string itemID;
    public Node nodeHit;

    [SerializeField] protected bool tutCorrectRotation;
    [SerializeField] protected bool tutCanPlace;

    /// <summary>
    /// Shoots a ray from mouse position then finds cloest walkable node
    /// </summary>
    public void nodeCheck()
    {
        RaycastHit hit;
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mousePos, out hit))
        {
            if (!hit.transform.CompareTag("Unplaced Item"))
            {
                nodeHit = gridRef.GetNodeFromWorldPoint(hit.point);

                if (nodeHit.placeable)
                {
                    gameObject.transform.position = new Vector3(nodeHit.worldPosition.x, nodeHit.worldPosition.y - 0.5f, nodeHit.worldPosition.z);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { eventData.pointerPress = gameObject; }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (hasItem)
        {
            TrapPlacement();
        }
    }

    protected void TrapPlacement()
    {
        if (tutCorrectRotation && tutCanPlace)
        {

            TutManager.tutInstance.NextStep();

            GameManager.instance.holdingItem = false;
            GameManager.instance.uiManager.RemoveShopOutline();
            GameManager.instance.audioManager.ItemPlacedAudio();
            GameManager.instance.currencyManager.ChangeCurrencyTut(itemID);

            nodeHit.placeable = false;
            hasItem = false;
            // need this sa if you let go the if statemnt in button inputs will destroy it 
            gameObject.tag = "Placed Item";
            placementEffect.Play();
        }
    }

    /// <summary>
    /// Controls placement and rotation of objects 
    /// </summary>
    protected void PlacementConfirmtation()
    {
        if (Input.GetKeyDown(KeyCode.R) | Input.GetMouseButtonDown(1) && hasItem)
        {
            gameObject.transform.Rotate(rotVector, Space.Self);
        }
    }
}