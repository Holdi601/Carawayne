using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScout : MonoBehaviour {

    void OnMouseDown()
    {
        if (SceneHandler.activeCompanion.HasActionOutstanding)
        {
            SceneHandler.scoutingActive = true;
            SceneHandler.activeCompanion.HasActionOutstanding = false;
        }
        ClickWorker.destroyRadialMenu();
    }
}
