using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class GizmosController : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private GameObject isDrag;
    public Camera cam;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }


    void OnMouseDrag()
    {
        Vector3 tempVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

        Vector3 curScreenPoint = new Vector3(tempVec.x, tempVec.y, screenPoint.z);

        // Vector3 tempVec = new Vector3(curScreenPoint.x, Input.mousePosition.z, curScreenPoint);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

}

/*


  private Vector3 mOffset;
private float mZCoord;


private void OnMouseDrag()
{
    transform.position = GetMouseWorldPos() + mOffset; 
}

private void OnMouseDown()
{
    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

    mOffset = gameObject.transform.position - GetMouseWorldPos(); 
}

Vector3 GetMouseWorldPos()
{
    Vector3 mousePoint = Input.mousePosition;

    mousePoint.z = mZCoord;

    return Camera.main.ScreenToWorldPoint(mousePoint);
}
*/

