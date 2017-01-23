using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull : MonoBehaviour {

    void OnMouseDown()
    {
        Companion c = gameObject.transform.parent.GetComponentInChildren<Companion>();
        c.ProviantDemand += 1;

    }
}
