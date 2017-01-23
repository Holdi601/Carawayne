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
        HasActionOutstanding = true;
    }

    public int fight(Opponent _target)
    {
        
        int rolledValue = SceneHandler.rollDice(diceValue);

        Debug.Log(meepleName + " hits " + _target.meepleName + " with a " + rolledValue + ". Dist = ");

        _target.damaged(rolledValue);
        HasActionOutstanding = false;
        //TODO:
        return rolledValue;
    }
}
