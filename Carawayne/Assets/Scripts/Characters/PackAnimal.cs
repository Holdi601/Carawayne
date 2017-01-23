using System;
using System.Collections.Generic;
using UnityEngine;

public class PackAnimal : Companion
{

    private int actualLoad;
    public int loadCapacity;
    private int butcherAmount;

    //Todo: change Pos to struct
    void Awake()
    {
        actualLoad = 30;
        loadCapacity = 30;
        butcherAmount = 10;
        proviantDemand = 0;
        ProviantDemandMax = 0;
        walkRange = 2;
        HasActionOutstanding = true;
    }

    public int butcher()
    {
        Alive = false;
        return butcherAmount;
    }

    //Loads an amount of proviant and returns overload value
    public int load(int _amount)
    {
        int rest = Math.Max((actualLoad + _amount) - loadCapacity, 0);
        ActualLoad += _amount;
        return Math.Abs(rest);
    }

    //Unloads an amount of proviant and returns overload value
    public int unload(int _amount) { 
            
        int rest = Math.Min((actualLoad - _amount), 0);
        ActualLoad -= _amount;
        return Math.Abs(rest);
    }

    public bool isGuided()
    {
        List<HexaPos> guidedPoses = getGuidedPos();
        bool isGuided = false;

        foreach (var guidePos in guidedPoses)
        {
            innerTile guidedTile = SceneHandler.smallMap[guidePos.x, guidePos.y].GetComponent<innerTile>();

            if (guidedTile.meep != null && (guidedTile.meep.GetType().IsSubclassOf(typeof(Companion))))
            {
                isGuided = true;
            }
        }

        return isGuided;
    }

    public List<HexaPos> getGuidedPos()
    {
        List<HexaPos> guidedTiles = new List<HexaPos>();
        HexaPos guidePos = Map.getPositionDirectionalByDistance(new HexaPos(Pos.x, Pos.y), 1, 1);
        guidedTiles.Add(guidePos);
        guidePos = Map.getPositionDirectionalByDistance(new HexaPos(Pos.x, Pos.y), 2, 1);
        guidedTiles.Add(guidePos);
        return guidedTiles;
    }

    //GETTER & SETTER
    //---------------
    public int ActualLoad
    {
        get { return actualLoad; }
        set
        {
            actualLoad = value;

            if (actualLoad < 0)
            {
                Debug.Log(meepleName + " has not enough proviant!");
                actualLoad = 0;
            }
            else if (actualLoad > loadCapacity)
            {
                Debug.Log(meepleName + " is überladen!");
                actualLoad = loadCapacity;
            }
        }
    }
}
