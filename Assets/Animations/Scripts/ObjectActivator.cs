using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    private float timePassed = 0f;
    private bool objectActivated = false;

    void Update()
    {
        if (!objectActivated)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 40f)
            {
                objectToActivate.SetActive(true);
                objectActivated = true;
            }
        }
    }
}