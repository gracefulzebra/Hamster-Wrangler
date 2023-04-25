using UnityEngine;
using System.Collections;

public class HamsterAnimation : MonoBehaviour
{
    Animator animator;
    public GameObject mainHamster, shockHamster, shockDeathHamster, disintegrateHamster;
    public float shockAnimationDuration;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetInAirBool(bool inAir)
    {
        animator.SetBool("inAir", inAir);
    }

    public void SetShockedTrigger()
    {
        animator.SetTrigger("ShockAnimationTrigger");
        StartCoroutine(ShockModelToggle());
    }

    IEnumerator ShockModelToggle()
    {
        mainHamster.SetActive(false);
        shockHamster.SetActive(true);

        yield return new WaitForSeconds(shockAnimationDuration);

        mainHamster.SetActive(true);
        shockHamster.SetActive(false);
    }

    DisintegrationController[] disintegrationControllers;

    public void SetDisintegrateTrigger()
    {
        disintegrationControllers = GetComponentsInChildren<DisintegrationController>();

      //  GameObject[] bodyParts = GameObject.FindGameObjectsWithTag("Disintegrate");
        mainHamster.SetActive(false);
        disintegrateHamster.SetActive(true);

        foreach (DisintegrationController script in disintegrationControllers)
        {
             StartCoroutine(script.GetComponent<DisintegrationController>().Disintegrate());
        }
    }

    public void ExplosionDeathAnimation()
    {
        animator.SetTrigger("Blam");
    }
}
