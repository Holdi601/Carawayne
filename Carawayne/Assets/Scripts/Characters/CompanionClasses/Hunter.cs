using UnityEngine;
using System;

public class Hunter: Companion
{

    public int dice;
    public int diceValue;

    //public Hunter(HexaPos _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    //{
    //    dice = 1;
    //    diceValue = 6;
    //}

    void Awake()
    {
        
        proviantDemand = 1;
        proviantDemandMax = 1;
        strength = 4;
        strengthMax = 10;
        dice = 1;
        diceValue = 6;
        walkRange = 3;
        setFoodPackages_hpBar();
    }

    public int hunt(HuntedAnimal _target)
    {
        int dist = Map.distance(_target.Pos, Pos);
        int rolledValue = SceneHandler.rollDice(diceValue);

        hasActionOutstanding = false;

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
