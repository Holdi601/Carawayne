using UnityEngine;
using System.Collections.Generic;

//Todo: Rethink design of meeple. MonoBeahiour or not?!?!
public class Meeple : MonoBehaviour
{
    //Todo: change Pos to struct
    private HexaPos pos;
    //Todo: change Pos to struct
    private List<HexaPos> walkableTiles;
    public int walkRange;
    private bool alive;
    public string meepleName;

    void Awake()
    {
        alive = true;
        pos = new HexaPos(0,0);
        walkRange = 4;
        meepleName = "StandardName";
        walkRange = 3;
    }

    public void moveTo(HexaPos _pos)
    {
        pos = _pos;
    }

    public virtual void init(HexaPos _pos, string _meepleName)
    {
        alive = true;
        pos = _pos;
        walkRange = 4;
        meepleName = _meepleName;
        Debug.Log(GetType() + " created on Position " + pos.x + "|" + pos.y);

    }

    //GETTER & SETTER
    //---------------
    public List<HexaPos> WalkableTiles
    {
        get {
            walkableTiles = new List<HexaPos>();
            for (int x = -walkRange; x < walkRange; x++)
            {
                for (int y = -walkRange; y < walkRange; y++)
                {
                    walkableTiles.Add(new HexaPos(pos.x + x, pos.y + y));
                }
            }
            return walkableTiles;
        }
    }

    public HexaPos Pos
    {
        get { return pos; }
        set { pos = value; }
    }

    public bool Alive
    {
        get { return alive; }
        set {
            alive = value;
            Debug.Log(meepleName + " died!");
            SceneHandler.meeples.Remove(this);
            Destroy(gameObject);
            //Todo: Do kill implementation stuff
        }
    }
}
