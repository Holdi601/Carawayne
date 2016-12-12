using UnityEngine;
using System.Collections.Generic;

//Todo: Rethink design of meeple. MonoBeahiour or not?!?!
public class Meeple
{

    private Vector2 pos;
    private List<Vector2> walkableTiles;
    public int walkRange;
    public bool alive;
    public string meepleName;

    public Meeple()
    {
        alive = true;
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
}
