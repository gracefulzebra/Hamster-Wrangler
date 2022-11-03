using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnap : MonoBehaviour
{
    
    private Grid grid;
    bool onGround;
   
    // Start is called before the first frame update
    void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            Vector3Int cp = grid.LocalToCell(transform.localPosition);
            transform.localPosition = grid.GetCellCenterLocal(cp);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Tile_8x8")
        {
            onGround = true;
            print("on");
        }
        else
        {
            onGround = false;
            print("off");
        }
    }
}
