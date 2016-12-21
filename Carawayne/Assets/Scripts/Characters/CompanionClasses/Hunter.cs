using UnityEngine;
using System;

public class Hunter: Companion
{

    public int dice;
    public int diceValue;

    public Hunter(Vector2 _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    {
        dice = 1;
        diceValue = 6;
    }

    public int hunt(HuntedAnimal _target)
    {
        Vector2 distVec = (_target.Pos - Pos);
        int dist = (int)(Math.Abs(distVec.x) + Math.Abs(distVec.y));
        int rolledValue = SceneHandler.rollDice(diceValue);

        hasActionOutstanding = false;

        //Todo: MapTiles distance problem. get Distance
        if (rolledValue >= dist)
        {
            Debug.Log("Hunter hits " + _target.meepleName + " with a " + rolledValue);
            _target.Alive = false;
            return _target.food;
        }

        Debug.Log("Hunter misses " + _target.meepleName + " with a " + rolledValue);
        return 0;
    }

}
