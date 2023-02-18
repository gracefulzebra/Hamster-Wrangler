using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class HamsterBase : MonoBehaviour
{
    [Header("Hamster Health")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHamsterHealth;

    [Header("Hamster Movement")]
    private Rigidbody _rb;
    public float speed;
    public float maxSpeed;
    private Vector3 direction;
    private Quaternion lookRotation;
    private Transform currentTarget;
    private Vector3 currentWaypoint;
    [SerializeField] private float rotationSpeed;
    
    [Header("Pathfinding")]
    [SerializeField] private Transform[] checkPoints;
    private Transform target;
    private Vector3[] path;
    private int targetIndex;
    private bool pathRequested = true;
    private int checkPointIndex = 0;

    [Header("Misc.")]
    [SerializeField] private ParticleSystem bloodAffect;

    [HideInInspector]
    public string hamsterID = "baseHamster";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Target").transform;
        currentHealth = maxHamsterHealth;
        speed = maxSpeed;
    }

    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, currentTarget.position, OnPathFound, this.gameObject);
    }

    private void Update()
    {
        MoveToTarget();
        UpdateCheckPoints();
    }

    private void MoveToTarget()
    {
        //Move toward
        direction = (currentWaypoint - transform.position).normalized;
        _rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Acceleration);

        //Turn toward
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); 
    }

    private void UpdateCheckPoints()
    {
        if (currentTarget == target)
            return;

        if (Distance(transform.position, currentTarget.position) < 0.4f)
        {
            checkPointIndex++;

            if (checkPointIndex == checkPoints.Length)
            {
                currentTarget = target;
            }
            else
            {
                currentTarget = checkPoints[checkPointIndex];
            }

            PathRequestManager.RequestPath(transform.position, currentTarget.position, OnPathFound, this.gameObject);
        }
    }

    public void SetCheckPoints(Transform[] _checkPoints)
    {
        checkPoints = _checkPoints;

        if (checkPoints.Length > 0)
        {
            currentTarget = checkPoints[0];
        }
        else
        {
            currentTarget = target;
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        pathRequested = false;

        if (pathSuccessful)
        {
            path = newPath;

            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
            currentWaypoint = path[0];

        targetIndex = 0;
        while (true)
        {
            float distance = Distance(transform.position, currentWaypoint);
            if (distance < 0.4f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            if (distance > 2f && !pathRequested)
            {
                pathRequested = true;
                PathRequestManager.RequestPath(transform.position, currentTarget.position, OnPathFound, this.gameObject);
            }
            yield return null;
        }
    }

    /// <summary>
    /// Calculates Euclidean Dist.
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    float Distance(Vector3 currentPos, Vector3 targetPos)
    {
        float a = targetPos.x - currentPos.x;
        float b = targetPos.z - currentPos.z;
        return Mathf.Sqrt(a * a + b * b);
    }

    /// <summary>
    /// Reduces hamster health by a value and checks to see if the health is below 0 for kill condition.
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    /// <summary>
    /// Destroys the current hamster without updating the score
    /// Used For Cheese
    /// </summary>
    public void Despawn()
    {
        GetComponent<HamsterScore>().UpdateWaveManager();
        Destroy(gameObject);
    }

    ///<summary>
    ///Destroys the current Hamster with no delay
    ///</summary>
    public void Kill()
    {
        Vector3 deathPoint = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        GameManager.instance.audioManager.PlayHamsterDeathAudio();
        Instantiate(bloodAffect, deathPoint, Quaternion.identity);
        GetComponent<HamsterScore>().SendData();
        Destroy(gameObject);
    }
}
