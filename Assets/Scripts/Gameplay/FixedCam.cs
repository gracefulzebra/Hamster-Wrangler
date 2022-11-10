using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCam : MonoBehaviour
{

    public List<Transform> cameraPos = new List<Transform>();
    int count;

    void Start()
    {
        // spawns camera at first camera position
        transform.position = cameraPos[0].transform.position;
        transform.rotation = cameraPos[0].transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // when you press direction key 
        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // checks if past bounds of list 
            if (count == cameraPos.Count - 1)
            {
                // if it is resets the counter so it continues to next pos without having to go all the way back round manuelly
                count = 0;
                transform.position = cameraPos[count].transform.position;
                transform.rotation = cameraPos[count].transform.rotation;
            }
            else
            {
                // changes the camera pos and rot to view the scene
                transform.position = cameraPos[count + 1].transform.position;
                transform.rotation = cameraPos[count + 1].transform.rotation;
                count += 1;
            }
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
                transform.rotation = cameraPos[count].transform.rotation;
            }
            else
            {
                // changes the camera pos and rot to view the scene
                transform.position = cameraPos[count - 1].transform.position;
                transform.rotation = cameraPos[count - 1].transform.rotation;
                count -= 1;
            }

        }
    }
}
