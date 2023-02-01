using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictedPathRenderer : MonoBehaviour
{
    Transform target;
    Vector3[] path;
    GridGenerator grid;

    [SerializeField] Transform[] checkPoints;
    Transform currentTarget;
    Vector3 lastTarget;
    int checkPointIndex = 0;

    LineRenderer pathLine;
    [SerializeField] LineRenderer linePrefab;

    Node oldPos;

    private void Awake()
    {
        target = GameObject.Find("Target").transform;
        grid = GameObject.Find("OliverGriddy").transform.GetComponent<GridGenerator>();

        GameObject[] tempChckPnts = GameObject.FindGameObjectsWithTag("CheckPoint");
        checkPoints = new Transform[tempChckPnts.Length];

        for (int i = 0; i < tempChckPnts.Length; i++)
        {
            checkPoints[i] = tempChckPnts[i].transform;
        }
    }

    void Start()
    {
        oldPos = grid.GetNodeFromWorldPoint(this.transform.position);
        CalculatePath();
        StartCoroutine(UpdatePath());
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
        }
    }

    IEnumerator UpdatePath()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(0.5f);
            Node currentPos = grid.GetNodeFromWorldPoint(transform.position);
            
            if (currentPos != oldPos && currentPos.walkable)
            {
                oldPos = currentPos;
                
                Destroy(pathLine.gameObject);
                
                CalculatePath();
            }
        }
    }
}
