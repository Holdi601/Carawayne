using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScout : MonoBehaviour {

    void OnMouseDown()
    {
        if (SceneHandler.activeCompanion.hasActionOutstanding)
        {
            SceneHandler.scoutingActive = true;
            SceneHandler.activeCompanion.hasActionOutstanding = false;
        }
        ClickWorker.destroyRadialMenu();
    }
}
