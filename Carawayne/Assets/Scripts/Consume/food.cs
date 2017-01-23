using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour {


	void OnMouseDown()
    {
        Companion c = gameObject.transform.parent.GetComponentInChildren<Companion>();
        c.ProviantDemand -= 1;
    }
}
