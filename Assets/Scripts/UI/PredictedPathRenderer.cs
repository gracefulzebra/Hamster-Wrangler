using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictedPathRenderer : MonoBehaviour
{
    Transform target;
    Vector3[] path;
    GridGenerator grid;

    private Transform[] checkPoints;
    Transform currentTarget;
    Vector3 lastTarget;
    int checkPointIndex = 0;

    LineRenderer pathLine;
    [SerializeField] LineRenderer linePrefab;

    [SerializeField] GameObject hamsterFootprintPrefab;
    List<GameObject> hamsterFootprints;

    Node oldPos;

    private void Awake()
    {
        target = GameObject.Find("Target").transform;
        grid = GameObject.Find("OliverGriddy").transform.GetComponent<GridGenerator>();

        hamsterFootprints = new List<GameObject>();

        if (GetComponent<HamsterSpawner>() != null)
            checkPoints = GetComponent<HamsterSpawner>().checkPoints;
    }

    void Start()
    {
        if (checkPoints.Length > 0)
            currentTarget = checkPoints[0];
        else
            currentTarget = target;

        oldPos = grid.GetNodeFromWorldPoint(this.transform.position);
        CalculatePath();
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = RepositionPath(newPath);
            DisplayPath();
        }
    }

    Vector3[] RepositionPath(Vector3[] _path)
    {
        for(int i = 0; i < _path.Length; i++)
        {
            _path[i].y = 0.1f;
        }

        return _path;
    }

    private void InitializePath()
    {
        pathLine = new LineRenderer();
        pathLine = Instantiate(linePrefab, this.transform);
        pathLine.positionCount = 0;
        checkPointIndex = 0;
        lastTarget = transform.position;
    }

    private void CalculatePath()
    {
        InitializePath();

        for (int i = 0; i < checkPoints.Length; i++)
        {
            PathRequestManager.RequestPath(lastTarget, checkPoints[i].position, OnPathFound, this.gameObject);
            lastTarget = checkPoints[i].position;
        }
        PathRequestManager.RequestPath(lastTarget, target.position, OnPathFound, this.gameObject);
    }

    private void DisplayPath()
    {
        pathLine.positionCount += path.Length;
        for (int i = 0; i < path.Length; i++)
        {
            pathLine.SetPosition(checkPointIndex, path[i]);
            checkPointIndex++;

            RaycastHit hitInfo;
            Physics.Raycast(path[i], -transform.up, out hitInfo);

            GameObject footprintInstance = Instantiate(hamsterFootprintPrefab, hitInfo.point + (Vector3.up * 0.03f), Quaternion.LookRotation(hitInfo.normal));

            Vector3 dir;

            if(i < path.Length - 1) { dir = path[i + 1] - path[i]; }
            else { dir = path[i] - path[i - 1]; }

            footprintInstance.transform.rotation = Quaternion.LookRotation(transform.up, dir.normalized);

            hamsterFootprints.Add(footprintInstance);
        }

        StartCoroutine(AnimationHandler());

    }

    IEnumerator AnimationHandler()
    {
        for(; ; )
        {
            StartCoroutine(AnimatePath());
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator AnimatePath()
    {
        for(int i = 0; i < hamsterFootprints.Count; i++)
        {
            hamsterFootprints[i].GetComponent<Animator>().SetTrigger("PlayAnim");
            yield return new WaitForSeconds(1.5f);
        }
    }
}
