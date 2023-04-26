using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FixedCam : MonoBehaviour
{
    public List<Transform> cameraPos = new List<Transform>();
    [SerializeField] Transform center;
    [SerializeField] int speed;
    [SerializeField] float cameraMovementDelay;
    int count;
    float timer;
    float inputCooldown;
    public LayerMask layerMask;

    void Start()
    {
        Camera.main.eventMask = layerMask;
        // spawns camera at first camera position
        transform.position = cameraPos[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        inputCooldown += Time.deltaTime;

          transform.LookAt(center);
          transform.position = Vector3.MoveTowards(transform.position, cameraPos[count].transform.position, speed * Time.deltaTime);
        // when you press direction key 
        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow) | Input.mouseScrollDelta.y > 0 && inputCooldown > cameraMovementDelay)
        {
            // checks if past bounds of list 
            if (count == cameraPos.Count - 1)
            {
                // if it is resets the counter so it continues to next pos without having to go all the way back round manuelly
                count = 0;
            }
            else
            {
                // changes the camera pos and rot to view the scene
                count += 1;          
            }
            inputCooldown = 0f;
        }
        // when you press direction key 
        if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow) | Input.mouseScrollDelta.y < 0 && inputCooldown > cameraMovementDelay)
        {
            // checks if past bounds of list 
            if (count == 0)
            {
                // if it is resets the counter so it continues to next pos without having to go all the way back round manuelly (MANUEL-ly??? MANIX MANUEL VAL GAMER???? MAUEL LOVE YOU???)
                count = 3;
            }
            else
            {
                // changes the camera pos and rot to view the scene
                count -= 1;
            }
            inputCooldown = 0f;
        }
    }
}