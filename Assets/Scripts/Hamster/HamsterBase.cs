using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class HamsterBase : MonoBehaviour
{
    public ParticleSystem bloodAffect;
    public Transform target;
    public float speed = 40;
    Vector3[] path;
    int targetIndex;
    Rigidbody _rb;
    Vector3 currentWaypoint;
    Vector3 direction;
    Quaternion lookRotation;
    public float rotationSpeed;
    [SerializeField] GridGenerator gridRef; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    private void Update()
    {
        direction = (currentWaypoint - transform.position).normalized;
        _rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Acceleration);
        GetRotation();
    }


    IEnumerator FollowPath()
    {
        currentWaypoint = path[0];

        while (true)
        {
            if(Distance(transform.position, currentWaypoint) < 0.5)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            yield return null;
        }
    }

    float Distance(Vector3 currentPos, Vector3 targetPos)
    {
        float a = targetPos.x - currentPos.x;
        float b = targetPos.z - currentPos.z;
        return Mathf.Sqrt(a * a + b * b);
    }

    private void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    void GetRotation()
    {
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    ///<summary>
    ///Destroys the current Hamster with no delay
    ///</summary>
    public void Kill()
    {
        Vector3 deathPoint = transform.position;
        Node nodeHit = gridRef.GetNodeFromWorldPoint(deathPoint);
        Vector3 bloodSpawn = new Vector3(nodeHit.worldPosition.x, nodeHit.worldPosition.y - 0.1f, nodeHit.worldPosition.z);
        Instantiate(bloodAffect, bloodSpawn, Quaternion.identity);
        Destroy(gameObject);
    }
}
