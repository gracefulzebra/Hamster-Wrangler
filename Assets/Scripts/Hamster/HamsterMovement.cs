using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HamsterMovement : MonoBehaviour
{

    public float speed;
    public Transform endPoint;

    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, step);
       // transform.position += transform.forward * speed;
    }
}
