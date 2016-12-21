using System;
using UnityEngine;
using System.Collections;

public class Mercenary : Companion
{

    public int dice;
    public int diceValue;

    public Mercenary(Vector2 _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    {
        dice = 1;
        diceValue = 6;
    }

    public int fight(Opponent _target)
    {
        Vector2 distVec = (_target.Pos - Pos);
        int dist = (int)(Math.Abs(distVec.x) + Math.Abs(distVec.y));
        int rolledValue = SceneHandler.rollDice(diceValue);

        hasActionOutstanding = false;

        //Todo: MapTiles distance problem. get Distance. Kampfsystem Konzept?!?!
        if (rolledValue >= dist)
        {
            Debug.Log(meepleName + " hits " + _target.meepleName + " with a " + rolledValue);
            _target.Alive = false;
            return rolledValue;
        }
        Debug.Log(meepleName + " misses " + _target.meepleName + " with a " + rolledValue);
        return rolledValue;
    }

}
