using System;
using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class Mercenary : Companion
{

    public int dice;
    public int diceValue;

    void Awake()
    {
        proviantDemand = 2;
        proviantDemandMax = 2;
        strength = 7;
        strengthMax = 10;
        dice = 1;
        diceValue = 6;
        walkRange = 1;
    }

    public int fight(Opponent _target)
    {
        
            int rolledValue = SceneHandler.rollDice(diceValue);

            hasActionOutstanding = false;

            Debug.Log(meepleName + " hits " + _target.meepleName + " with a " + rolledValue + ". Dist = ");

            _target.damaged(rolledValue);

            //Debug.Log(meepleName + " misses " + _target.meepleName + " with a " + rolledValue + ". Dist = " + dist);
            ////MissesAnimation...Event...
            //return rolledValue;
        //TODO:
        return rolledValue;
    }
}
