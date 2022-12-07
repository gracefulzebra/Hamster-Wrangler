using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        col.GetComponent<HamsterBase>().Kill();
    }
}
