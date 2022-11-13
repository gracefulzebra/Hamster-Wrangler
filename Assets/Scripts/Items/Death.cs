using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<HamsterBase>().Kill();
    }
}
