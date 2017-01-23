using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBehind : MonoBehaviour {

    // Use this for initialization
    void OnMouseDown()
    {
        
        SceneHandler.activeCompanion.Alive = false;
        ClickWorker.destroyRadialMenu();
    }
}
