using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{

    Queue<PathRequest> pathRequestsQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathRequestManager instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback, GameObject caller)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback, caller);  
        instance.pathRequestsQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestsQueue.Count > 0)
        {
            currentPathRequest = pathRequestsQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        if (currentPathRequest.caller != null)
        {
            currentPathRequest.callback(path, success);
        }
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;
        public GameObject caller;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback, GameObject _caller)
        {
            pathStart = _start;
            pathEnd = _end; 
            callback = _callback;
            caller = _caller;
        }
    }
}
