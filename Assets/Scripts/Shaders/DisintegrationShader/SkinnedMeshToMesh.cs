using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public VisualEffect VFXGraph;
    public float refreshrate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateVFXGraph());
    }
    IEnumerator UpdateVFXGraph()
    {
        while(gameObject.activeSelf)
        {
            Mesh m = new Mesh();
            meshRenderer.BakeMesh(m);
            VFXGraph.SetMesh("Mesh", m);


            yield return new WaitForSeconds(refreshrate);
        }
    }

}
