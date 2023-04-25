using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DisintegrationController : MonoBehaviour
{
    public Animator anim;
    public VisualEffect VFXgraph;
    public SkinnedMeshRenderer meshRendererBody;

    public float dissolveRate = 0.05f;
    public float refreshrate = 0.02f;

    private Material[] disintegrateMaterials;

    // Start is called before the first frame update
    void Start()
    {
        if(VFXgraph != null)
        {
            VFXgraph.Stop();
            VFXgraph.gameObject.SetActive(false);
        }

        if(meshRendererBody != null)
        {
            disintegrateMaterials = meshRendererBody.materials;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Disintegrate());
        }
    }

   public IEnumerator Disintegrate()
    {
        if (anim != null)
        {
            anim.SetTrigger("Burn");
        }
        yield return new WaitForSeconds(0.2f);

        if(VFXgraph != null)
        {
            VFXgraph.gameObject.SetActive(true);
            VFXgraph.Play();
        }

        float counter = 0;

        if(disintegrateMaterials.Length > 0)
        {
            while(disintegrateMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;

                for(int i=0; i<disintegrateMaterials.Length; i++)
                {
                    disintegrateMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshrate);
            }
        }
        VFXgraph.Stop();
    }
}
