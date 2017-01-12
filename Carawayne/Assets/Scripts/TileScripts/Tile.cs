using UnityEngine;
using System.Collections;

public enum specialTile { None, Lookout, Finish, Hostiles};
public enum tileType { Desert, Forrest, Mountain, Oasis };
public class Tile : MonoBehaviour {

    public tileType tile_type = tileType.Desert;
    public specialTile special = specialTile.None;
    public int genField;
    private Material bkpMat;
    private Material bkpMat_t;
 
   

 
}
