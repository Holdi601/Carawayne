using System;
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

            if (meep == null && SceneHandler.meeples.Contains(SceneHandler.activeCompanion))
            {
                SceneHandler.setMeeplePos(SceneHandler.activeCompanion.gameObject, new HexaPos(posX, posY));

                GameObject tileHolder = GameObject.Find("tileHolder");
                SoundHelper sh = tileHolder.GetComponent<SoundHelper>();
                sh.Play("run");
            }
            else if (meep == null && SceneHandler.activeMode == GameMode.PREPARATION)
            {
                Type compType = SceneHandler.activeCompanion.GetType();

                switch (compType.ToString())
                {
                    case "Mercenary":
                        SceneHandler.createMeeple<Mercenary>("Mercenary", new HexaPos(posX, posY));
                        break;
                    case "Hunter":
                        SceneHandler.createMeeple<Hunter>("Mercenary", new HexaPos(posX, posY));
                        break;
                    case "HuntedAnimal":
                        SceneHandler.createMeeple<HuntedAnimal>("Mercenary", new HexaPos(posX, posY));
                        break;
                    case "Healer":
                        SceneHandler.createMeeple<Healer>("Mercenary", new HexaPos(posX, posY));
                        break;
                    case "Scout":
                        SceneHandler.createMeeple<Scout>("Mercenary", new HexaPos(posX, posY));
                        break;
                    case "PackAnimal":
                        SceneHandler.createMeeple<PackAnimal>("Mercenary", new HexaPos(posX, posY));
                        break;
                    default:
                        break;
                }
            }
            else if (meep.GetType() == typeof(Opponent) && SceneHandler.activeTacticalGame.GetType() == typeof (BattleGround))
            {
                Mercenary merc = (Mercenary)SceneHandler.activeCompanion;
                
                int dist = Map.distance(meep.Pos, merc.Pos);
                int rnd = SceneHandler.rollDice(10);
                Debug.Log(rnd+" "+dist);
                if (rnd >= dist)
                {
                    merc.fight(meep.GetComponent<Opponent>());
                }
                merc.HasActionOutstanding = false;
            }
            else if (meep.GetType() == typeof(HuntedAnimal) && SceneHandler.activeTacticalGame.GetType()==typeof (HuntingGround))
            {
                Debug.Log("HUNT");
                Hunter hunter = (Hunter)SceneHandler.activeCompanion;
                SceneHandler.gainProviant(hunter.hunt(meep.GetComponent<HuntedAnimal>()));
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
