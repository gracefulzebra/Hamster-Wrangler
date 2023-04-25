using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawPrintManager : MonoBehaviour
{
    private Animator animator;   

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        animator.SetTrigger("PlayAnim");            
    }

    public void StopAnimation()
    {
       
    }

    

    
}
