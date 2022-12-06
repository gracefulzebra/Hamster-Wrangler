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
    private GameManager manager;
    // used to check if fit is a hamster enter colldiers
    // i imagien when we add in other types of hamsters that 
    // this can be reused for different effects
    public string hamsterID = "baseHamster";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        target = GameObject.Find("Target").transform;
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
            float distance = Distance(transform.position, currentWaypoint);
            if (distance < 0.25f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            if (distance > 2f)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
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
                Gizmos.DrawCube(path[i], new Vector3(1,1.5f,1));

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
        Vector3 deathPoint = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        manager.audioManager.PlayHamsterDeathAudio();
        Instantiate(bloodAffect, deathPoint, Quaternion.identity);
        GetComponent<HamsterScore>().SendData();
        Destroy(gameObject);
    }
}
