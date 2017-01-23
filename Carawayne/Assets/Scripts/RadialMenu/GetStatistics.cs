using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStatistics : MonoBehaviour {

    void OnMouseDown()
    {
        ClickWorker.destroyRadialMenu();
        SceneHandler.activeCompanion.openStatistics();
       
    }
}
