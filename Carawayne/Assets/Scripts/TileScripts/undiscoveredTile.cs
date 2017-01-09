﻿using UnityEngine;
using System.Collections;

public class undiscoveredTile : MonoBehaviour {

    public HexaPos position;
    public bool clicked = false;
    Material backup;
    bool highlighted = false;

    void OnMouseDown()
    {
        SceneHandler.clickMaptile(position);
    }

    public void highlight(bool h)
    {
        if (h)
        {
            if (!highlighted)
            {
                backup = gameObject.GetComponent<MeshRenderer>().material;
                gameObject.GetComponent<MeshRenderer>().material = Initialisation.highlightMate;
                highlighted = true;
            }
        }
        else
        {
            if (highlighted)
            {
                gameObject.GetComponent<MeshRenderer>().material = backup;
                highlighted = false;
            }
        }
    }
}
