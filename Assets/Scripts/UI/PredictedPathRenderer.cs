using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictedPathRenderer : MonoBehaviour
{
    Transform target;
    Vector3[] path;
    GridGenerator grid;

    LineRenderer pathLine;
    [SerializeField] LineRenderer linePrefab;

    Node oldPos;

    private void Awake()
    {
        target = GameObject.Find("Target").transform;
        grid = GameObject.Find("OliverGriddy").transform.GetComponent<GridGenerator>();
    }

    void Start()
    {
        oldPos = grid.GetNodeFromWorldPoint(this.transform.position);
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound, this.gameObject);
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = RepositionPath(newPath);
            RenderPath();
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
    private void RenderPath()
    {
        pathLine = new LineRenderer();
        pathLine = Instantiate(linePrefab, this.transform);

        pathLine.positionCount = path.Length;
        pathLine.SetPositions(path);        
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
                
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound, this.gameObject);
            }
        }
    }
}
