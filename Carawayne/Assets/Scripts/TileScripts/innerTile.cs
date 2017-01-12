using UnityEngine;
using System.Collections;

public class innerTile : MonoBehaviour {

    public int posX;
    public int posY;
    public Meeple meep;
    int clicks=0;
    public bool isActive;

    void OnMouseDown()
    {
        if (isActive)
        {
            SceneHandler.setMeeplePos(SceneHandler.activeCompanion.gameObject, new HexaPos(posX, posY));
            SceneHandler.activeCompanion = null;
            Map.highlightAllInnerTiles(false);
        }
    }

    public void setHighlighted(bool _isHighlighted)
    {
        if (_isHighlighted)
        {
            GetComponent<MeshRenderer>().material = Initialisation.innerTileActiveMate;
        }
        else
        {
            GetComponent<MeshRenderer>().material = Initialisation.innerTileMate;
        }
        
        isActive = _isHighlighted;
    }

    void resetClicks()
    {
        
    }
}
