using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHeal : MonoBehaviour {

    void OnMouseDown()
    {
        if (SceneHandler.activeCompanion.hasActionOutstanding)
        {
            SceneHandler.healingActive = true;
            SceneHandler.activeCompanion.hasActionOutstanding = false;
            SceneHandler.activeCompanion.Strength -= 1;
        }
        ClickWorker.destroyRadialMenu();
    }

}
