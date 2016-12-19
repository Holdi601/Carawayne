using UnityEngine;
using System.Collections.Generic;

//Todo: Rethink design of meeple. MonoBeahiour or not?!?!
public class Meeple
{

    private Vector2 pos;
    private List<Vector2> walkableTiles;
    public int walkRange;
    private bool alive;
    public string meepleName;

    public Meeple(Vector2 _pos, string _meepleName)
    {
        alive = true;
        pos = _pos;
        walkRange = 4;
        meepleName = _meepleName;
    }

    public void moveTo(Vector2 _pos)
    {
        pos = _pos;
    }

    //GETTER & SETTER
    //---------------
    public List<Vector2> WalkableTiles
    {
        get {
            walkableTiles = new List<Vector2>();
            for (int x = -walkRange; x < walkRange; x++)
            {
                for (int y = -walkRange; y < walkRange; y++)
                {
                    walkableTiles.Add(new Vector2(pos.x + x, pos.y + y));
                }
            }
            return walkableTiles;
        }
    }

    public Vector2 Pos
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
            //Todo: Do kill implementation stuff
        }
    }
}
