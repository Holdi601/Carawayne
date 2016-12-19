using UnityEngine;
using System.Collections;

public class TacticalMapTile : MonoBehaviour
{

    public Vector2 pos;
    public Meeple mountedMeeple;

	// Use this for initialization
	void Start ()
	{
	    mountedMeeple = null;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        //Companion comp = new Hunter(pos, "Bernd", 2, 10);
    }
}
