using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FixedCam : MonoBehaviour
{

    public List<Transform> cameraPos = new List<Transform>();
    int count;
    public int speed;
    float t;
 public   bool moveToNewPos;
    public Transform center;
     public Vector3 nextPos;

    void Start()
    {
        // spawns camera at first camera position
        transform.position = cameraPos[0].transform.position;
        transform.rotation = cameraPos[0].transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        print(count);
        transform.LookAt(center);
        t = speed * Time.deltaTime; // calculate distance to move
        if (moveToNewPos)//&& count != cameraPos.Count - 1)
        {      
            transform.position = Vector3.MoveTowards(transform.position, cameraPos[count + 1].transform.position, t);
            if (transform.position == nextPos)
            {
                moveToNewPos = false;
            }       
        } 
       // else if (moveToNewTrans && count == cameraPos.Count - 1)
        {
         //   count = 0;
         //   transform.position = Vector3.MoveTowards(transform.position, cameraPos[count].transform.position, t);
        }
        // when you press direction key 
        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // checks if past bounds of list 
            if (count == cameraPos.Count - 1)
            {
                // if it is resets the counter so it continues to next pos without having to go all the way back round manuelly
                nextPos = cameraPos[count].transform.position;
                moveToNewPos = true;
                count = 0;
            }
            else
            {
                // changes the camera pos and rot to view the scene
                nextPos = cameraPos[count + 1].transform.position;
                moveToNewPos = true;
                count += 1;
            }
         //   moveToNewTrans = false;
        }
        // when you press direction key 
        if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
        {
            // checks if past bounds of list 
            if (count == 0)
            {
                // if it is resets the counter so it continues to next pos without having to go all the way back round manuelly (MANUEL-ly??? MANIX MANUEL VAL GAMER???? MAUEL LOVE YOU???)
                count = 3;
                transform.position = cameraPos[count].transform.position;
               // transform.rotation = cameraPos[count].transform.rotation;
            }
            else
            {
                // changes the camera pos and rot to view the scene
                transform.position = cameraPos[count - 1].transform.position;
                //transform.rotation = cameraPos[count - 1].transform.rotation;
                count -= 1;
            }
        }
    }

  
       
    

    IEnumerator RemoveTar()
    {
        yield return 2;
        transform.position = Vector3.MoveTowards(transform.position, cameraPos[count + 1].transform.position, t);
 
    }

}
/*
public IEnumerator MoveOverSeconds1(GameObject objectToMove, Vector3 end, float seconds)
{
    float elapsedTime = 0;
    Vector3 startingPos = objectToMove.transform.position;
    while (elapsedTime < seconds)
    {
        objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
        elapsedTime += Time.deltaTime;
        yield return new WaitForEndOfFrame();
    }
    objectToMove.transform.position = end;
}

public IEnumerator MoveOverSeconds()
{
    float seconds = 0;
    float elapsedTime = 0;
    Vector3 end = cameraPos[count + 1].transform.position;
    while (elapsedTime < seconds)
    {

        transform.position = Vector3.MoveTowards(transform.position, cameraPos[count + 1].transform.position, speed);
        elapsedTime += Time.deltaTime;
        yield return new WaitForEndOfFrame();
    }
    transform.position = end;
}
*/