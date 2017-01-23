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
            Map.highlightAllInnerTiles(false);
            if (meep == null)
            {
                SceneHandler.setMeeplePos(SceneHandler.activeCompanion.gameObject, new HexaPos(posX, posY));

                GameObject tileHolder = GameObject.Find("tileHolder");
                SoundHelper sh = tileHolder.GetComponent<SoundHelper>();
                sh.Play("run");
            }
            else if (meep.GetType() == typeof(Opponent) && SceneHandler.activeTacticalGame.GetType() == typeof (BattleGround))
            {
                Mercenary merc = (Mercenary)SceneHandler.activeCompanion;
                merc.fight(meep.GetComponent<Opponent>());
            }
            else if (meep.GetType() == typeof(HuntedAnimal) && SceneHandler.activeTacticalGame.GetType()==typeof (HuntingGround))
            {
                Debug.Log("HUNT");
                Hunter hunter = (Hunter)SceneHandler.activeCompanion;
                hunter.hunt(meep.GetComponent<HuntedAnimal>());
            }

        SceneHandler.activeCompanion = null;

        }
    }

    public void setHighlighted(bool _isHighlighted)
    {
        isActive = _isHighlighted;

        if (_isHighlighted && meep == null)
        {
            GetComponent<MeshRenderer>().material = Initialisation.innerTileActiveMate;
                    }
        else if(_isHighlighted && meep != null)
        {
            if (!meep.GetType().IsSubclassOf(typeof (Companion)))
            {
                GetComponent<MeshRenderer>().material = Initialisation.lookOutMate;
            }
            else
            {
                GetComponent<MeshRenderer>().material = Initialisation.innerTileMate;
                isActive = false;
            }
        }
        else
        {
            GetComponent<MeshRenderer>().material = Initialisation.innerTileMate;
        }
    }

    void resetClicks()
    {
        
    }
}
