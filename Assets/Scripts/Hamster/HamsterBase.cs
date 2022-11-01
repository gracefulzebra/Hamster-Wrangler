using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterBase : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform endPoint;
    Rigidbody _rb;
    Vector3 movement;
    public float spd;
    public List<Transform> movementPoints;
    Vector3 currentPoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {   
        currentPoint = GetCurrentPoint();
    }
    private void Update()
    {
        movement = (currentPoint - transform.position).normalized * spd;

        if((transform.position - currentPoint).magnitude < 2f && movementPoints.Count > 0)
        {
            currentPoint = GetCurrentPoint(); 
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(movement, ForceMode.Impulse);
    }

    /// <summary>
    /// Finds the next target pathfinding point from the list of points 
    /// and removes the elements from the list as they are achieved
    /// </summary>
    /// <returns>
    /// Vector3 for next target position
    /// </returns>
    Vector3 GetCurrentPoint()
    {
        Vector3 temp = movementPoints[0].position;
        movementPoints.RemoveAt(0);
        return temp;
    }

    ///<summary>
    ///Destroys the current Hamster with no delay
    ///</summary>
    public void Kill()
    {
        Destroy(gameObject);
    }
}
