using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHeal : MonoBehaviour {

    void OnMouseDown()
    {
        if (SceneHandler.activeCompanion.HasActionOutstanding)
        {
            SceneHandler.healingActive = true;
            SceneHandler.activeCompanion.HasActionOutstanding = false;
            SceneHandler.activeCompanion.Strength -= 1;
        }
        ClickWorker.destroyRadialMenu();
    }

}
