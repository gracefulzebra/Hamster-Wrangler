using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToActivate : MonoBehaviour
{

    public bool turnedOn;

    private void OnMouseDown()
    {
        if (!turnedOn)
            turnedOn = true;
    }
}
