using UnityEngine;
using System.Collections;


public enum tileType { Desert, Forrest, Mountain, Oasis };
public class Tile : MonoBehaviour {

    public tileType tile_type = tileType.Desert;
    private Material bkpMat;
    private Material bkpMat_t;
    bool highlighted=false;
    bool visited = false;
    
   

    public void highlight(bool h)
    {

        MeshRenderer mr = gameObject.transform.FindChild("undiscovered").GetComponent<MeshRenderer>();
        MeshRenderer mr2 = gameObject.transform.FindChild("actualTile").GetComponent<MeshRenderer>();

        if (h && !highlighted)
        {
            bkpMat = mr.material;
            bkpMat_t = mr2.material;
            mr.material = Initialisation.highlightMate;
            mr2.material = Initialisation.highlightMate;
            highlighted = true;
        }else
        {
            if(bkpMat!=null && bkpMat_t != null)
            {
                mr.material = bkpMat;
                mr2.material = bkpMat_t;
                highlighted = false;
            }
            
        }
    }



}
