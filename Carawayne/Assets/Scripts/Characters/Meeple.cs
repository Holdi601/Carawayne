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
        walkRange = 1;
        meepleName = "StandardName";
    }

    public void moveTo(HexaPos _pos)
    {
        
    }

    public void moveTowardsTarget(HexaPos _targetPos)
    {
        for (int i = 0; i < walkRange; i++)
        {
            int dist = Map.distance(pos, _targetPos);

            if (dist <= 0)
                break;

            int dir = Map.getDirection(pos, _targetPos);
            HexaPos moveTarget = Map.tilesInRange(pos, 1)[dir];
            SceneHandler.setMeeplePos(this.gameObject, moveTarget);
            Debug.Log(meepleName + "moved!!!to" + moveTarget + "DIRECTION:");
        }
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
