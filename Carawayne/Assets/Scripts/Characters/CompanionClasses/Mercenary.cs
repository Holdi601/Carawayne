using System;
using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class Mercenary : Companion
{

    public int dice;
    public int diceValue;

    //public Mercenary(HexaPos _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    //{
    //    dice = 1;
    //    diceValue = 6;
    //}

    void Awake()
    {
        proviantDemand = 2;
        proviantDemandMax = 2;
        strength = 6;
        strengthMax = 10;
        dice = 1;
        diceValue = 6;
    }

    public int fight(Opponent _target)
    {
        int dist = Map.distance(_target.Pos, Pos);
        int rolledValue = SceneHandler.rollDice(diceValue);

        hasActionOutstanding = false;

        if (rolledValue > dist)
        {
            Debug.Log(meepleName + " hits " + _target.meepleName + " with a " + rolledValue + ". Dist = " + dist);
            _target.Alive = false;
            return rolledValue;
        }

        Debug.Log(meepleName + " misses " + _target.meepleName + " with a " + rolledValue + ". Dist = " + dist);
        //MissesAnimation...Event...
        return rolledValue;
    }
}
