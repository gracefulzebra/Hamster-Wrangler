using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;




public class HamsterBase : MonoBehaviour
{
    [Header("Hamster Health")]
    [SerializeField] public int currentHealth;
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
    [SerializeField] private GameObject[] bloodEffect;
    [SerializeField] private float maxDecalDistance = 5f;
    [SerializeField] private float decalOffsetDistance = 0.5f;
    [SerializeField] private float decalDuration = 1f;

    [Header("Ground Check")]
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    public bool isGrounded;

    [HideInInspector]
    public string hamsterID = "baseHamster";
    HamsterAnimation animatorController;
    HamsterMatChange materialChanger;

    public enum DeathTypes
    {
        Default,
        Fire,
        BugZapper
    };

   public DeathTypes deathType;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Target").transform;
        currentHealth = maxHamsterHealth;
        speed = maxSpeed;
        animatorController = GetComponent<HamsterAnimation>();
        materialChanger = GetComponent<HamsterMatChange>();
    }

    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, currentTarget.position, OnPathFound, this.gameObject);

     //   StartCoroutine(start());
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<HamsterAnimation>().SetDisintegrateTrigger();
    }

    bool playOnce;
    private void Update()
    {
        if (transform.position.y < 0.3f)
            MoveToTarget();
        UpdateCheckPoints();

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        animatorController.SetInAirBool(!isGrounded);
    }

    private void MoveToTarget()
    {
        //Move toward
        direction = (currentWaypoint - transform.position).normalized;
        _rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Acceleration);

        direction.y = this.transform.position.y;

        //Turn toward
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); 
    }

    private void UpdateCheckPoints()
    {
        if (currentTarget == target)
            return;

        if (Distance(transform.position, currentTarget.position) < 0.3f)
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
            if (distance < 0.45f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            if (distance > 1.5f && !pathRequested)
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

        HamsterDamageStates();
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
        if (gameObject.layer != 3)
        {
           GetComponent<HamsterScore>().SendData();
        }
        GameManager.instance.audioManager.PlayHamsterDeathAudio();
        HamsterDeathEffect();
    }

    public void WaterDeath()
    {
        StartCoroutine(WaterDeathTimer());
    }

   public IEnumerator WaterDeathTimer()
    {
        GetComponent<HamsterScore>().SendData();
        GameManager.instance.audioManager.PlayHamsterDeathAudio();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
   }

    private void HamsterDamageStates()
    {
        float pcntHealth = ((float)currentHealth / (float)maxHamsterHealth) * 100;

        if (pcntHealth >= 50 && pcntHealth < 75)
        {
            materialChanger.SetDamageState1();
        }
        else if(pcntHealth >= 25 && pcntHealth < 50)
        {
            materialChanger.SetDamageState2();
        }
        else if(pcntHealth < 25)
        {
            materialChanger.SetDamageState3();
        }

    }

    // play in kill, for different death aimation depending on death 
    void HamsterDeathEffect()
    {
        if (GetComponent<ItemEffects>().hasBeenShocked && GetComponent<ItemEffects>().onFire)
        {
            deathType = DeathTypes.BugZapper;
        }
        else if (GetComponent<ItemEffects>().hasBeenShocked)
        {
            deathType = DeathTypes.BugZapper;
        } 
        else if (GetComponent<ItemEffects>().onFire)
        {
            deathType = DeathTypes.Fire;
        }
        else
        {
            deathType = DeathTypes.Default;
        }
        switch (deathType)
        {
            case (DeathTypes.Default):
                GameManager.instance.vfxManager.HamsterDeathLimbSpawn(transform);
                CreateDecalEffects();

                Destroy(gameObject);
                break;
            case (DeathTypes.Fire):
                GetComponent<HamsterAnimation>().SetDisintegrateTrigger(); 

                StartCoroutine(DelayDeathFire());
                break;
            case (DeathTypes.BugZapper):
                StartCoroutine(GetComponent<HamsterAnimation>().SetShockedDeathTrigger());

                // changes later to one that hamsters dont touch
                gameObject.layer = 3;

                StartCoroutine(DelayDeathShock());
                break;
        }
    }

    IEnumerator DelayDeathFire()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator DelayDeathShock()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    private Ray GenerateRandomAngleRay()
    {
        Vector3 rayDirection = new Vector3(Random.Range(-10, 10), Random.Range(-40, -10), Random.Range(-10, 10));
                
        Ray newRay = new Ray(this.transform.position, rayDirection);

        return newRay;
    }

    void CreateDecalEffects()
    {
        RaycastHit hitData;
        Ray randomRay = GenerateRandomAngleRay();

        Physics.Raycast(randomRay, out hitData, maxDecalDistance);

        Vector3 decalPosition = hitData.point + (randomRay.direction.normalized * decalOffsetDistance);

         int randBlood = Random.Range(0, 4);

        GameObject decalInstance = Instantiate(bloodEffect[randBlood], decalPosition, Quaternion.LookRotation(hitData.normal));
    }
}
