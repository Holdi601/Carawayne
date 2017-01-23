using UnityEngine;
using System;

public class Hunter: Companion
{

    public int dice;
    public int diceValue;

    void Awake()
    {
        
        proviantDemand = 1;
        proviantDemandMax = 1;
        strength = 4;
        strengthMax = 10;
        dice = 1;
        diceValue = 6;
        walkRange = 3;

        HasActionOutstanding = true;

        setFoodPackages_hpBar();

    }

    public int hunt(HuntedAnimal _target)
    {
        int dist = Map.distance(_target.Pos, Pos);
        int rolledValue = SceneHandler.rollDice(diceValue);

        HasActionOutstanding = false;

        Debug.Log(rolledValue+"--"+dist);

        //Todo: MapTiles distance problem. get Distance
        if (rolledValue >= dist)
        {
            Debug.Log(meepleName + " hits " + _target.meepleName + " (distance: " + dist + " tiles) with a " + rolledValue);
            _target.Alive = false;
            return _target.food;
        }

        Debug.Log(meepleName + "misses " + _target.meepleName + " (distance: " + dist + " tiles) with a " + rolledValue);
        return 0;
    }

}
