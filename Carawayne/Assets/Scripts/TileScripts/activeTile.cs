using UnityEngine;
using System.Collections;

public class activeTile : MonoBehaviour {
    public HexaPos position;


	void OnMouseDown()
    {
        if(SceneHandler.activeMode== GameMode.EXPLORATION)
        {
            SceneHandler.endTurn();
        }
        
    }
}
