using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Companion : Meeple {

    public int proviantDemand;
    public int proviantDemandMax;
    public int strength;
    public int strengthMax;
    private float proviantRation;
    private bool hasActionOutstanding;

    //public Companion(HexaPos _pos, string _name, int _proviantDemand, int _strength): base(_pos, _name)
    //{
    //    proviantDemand = _proviantDemand;
    //    strength = _strength;
    //    strengthMax = _strength;
    //    proviantDemandMax = _proviantDemand;
    //    ProviantRation = 1.0f;
    //    hasActionOutstanding = true;
    //}

    public void initFoo()
    {
        ProviantRation = 1.0f;
        hasActionOutstanding = true;
        walkRange = 1;
        Debug.Log("AWAKE COMPANION");
    }

    void Awake()
    {
        ProviantRation = 1.0f;
        hasActionOutstanding = true;
        walkRange = 1;
        Debug.Log("AWAKE COMPANION");
    }

    public void moveTo(HexaPos _pos)
    {
        //Todo: Missing implementation movement
        hasActionOutstanding = false;

    }

    public void doAction(/*Action _action*/)
    {
        //Todo: Implement if decide to generic action instead of individual subClass actions (fight, heal, etc...)
        //Todo: OnPlayerInteractionEnded trigger for tactical game. Raise Event OnPlayerInteraction
        hasActionOutstanding = false;
        
    }

    //Consume a proviant ration
    public int consumeRation()
    {
        //Loose strength depending on difference between ProviantDemand and ProviantDemandMax
        Strength -= (proviantDemandMax - proviantDemand);

        //Return consumed proviant
        return proviantDemand;
    }

    public void damaged(int _amount)
    {
        //If companion already on 0 aka "bewusstlos". --> Kill companion.
        if (strength <= 0 && _amount > 0)
        {
            Alive = false;
        }
        else
        {
            Strength -= _amount;
        }
    }

    //GETTER & SETTER
    //---------------
    public int Strength
    {
        get { return strength; }
        set {
            strength = Math.Max(value, 0);
            strength = Math.Min(value, strengthMax);
        }
    }

    public float ProviantRation
    {
        get { return proviantRation; }
        set {
            proviantRation = Math.Max(value, 0);
            proviantRation = Math.Min(value, 2);
            //If ProviantRation changes --> change proviantDemand
            proviantDemand = (int)(proviantDemand * proviantRation);
        }
    }

    public int ProviantDemand
    {
        get { return proviantDemand; }
        set
        {
            proviantDemand = Math.Max(value, 0);
            proviantDemand = Math.Min(value, proviantDemandMax);
        }
    }

    void OnMouseDown()
    {
        
        if (SceneHandler.activeMode== GameMode.EXPLORATION)
        {
            activateCompanion(false);
        }
        else if (SceneHandler.activeTacticalGame.GetType()==typeof(HuntingGround))
        {
            if (GetType()==typeof(Hunter))
            {
                activateCompanion();
            }
        }
        else if (SceneHandler.activeTacticalGame.GetType() == typeof (BattleGround))
        {
            if (GetType() == typeof (Mercenary))
            {
                activateCompanion();
            }
        }
        
    }

    public void activateCompanion(bool _respectWalkRange=true)
    {
        SceneHandler.activeCompanion = this;

        if (hasActionOutstanding)
        {
            if (_respectWalkRange)
            {
                List<HexaPos> walkableTilesPos = new List<HexaPos>();
                walkableTilesPos = Map.tilesInRange(Pos, walkRange);

                Map.highlightAllInnerTiles(false);
                foreach (HexaPos tilePos in walkableTilesPos)
                {
                    SceneHandler.smallMap[tilePos.x, tilePos.y].GetComponent<innerTile>().setHighlighted(true);
                }
            }
            else
            {
                Map.highlightAllInnerTiles(true);
            }

            MeshRenderer innerTMesh = SceneHandler.smallMap[Pos.x, Pos.y].GetComponent<MeshRenderer>();
            innerTMesh.material = Initialisation.lookOutMate;
        }
    }

    public bool HasActionOutstanding
    {
        get { return hasActionOutstanding; }
        set
        {
            hasActionOutstanding = value;

            if (!hasActionOutstanding)
            {
                SceneHandler.activeTacticalGame.onPlayerInteractionEnded();
            }
        }
    }
}
