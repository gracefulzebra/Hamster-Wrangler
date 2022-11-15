using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : MonoBehaviour
{

    public GameObject winText;

    private void OnTriggerEnter(Collider col)
    {
        winText.SetActive(true);
    }

}
