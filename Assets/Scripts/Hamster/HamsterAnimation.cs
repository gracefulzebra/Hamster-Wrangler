using UnityEngine;

public class HamsterAnimation : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void RakeLaunchAnimation()
    {
        animator.SetTrigger("RakeAnimationTrigger");
    }
}
