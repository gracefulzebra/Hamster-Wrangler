using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<HamsterBase>() != null)
        col.GetComponent<HamsterBase>().Kill();
    }
}
