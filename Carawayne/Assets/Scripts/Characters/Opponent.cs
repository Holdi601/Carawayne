using System;
using UnityEngine;

public class Opponent : Meeple
{

    public Meeple targetMeeple;
    public int dice;
    public int diceValue;
    public int strength;
    public int strengthMax;

    void Awake()
    {
        dice = 1;
        diceValue = 6;
        strength = 1;
        strengthMax = strength;
        walkRange = 1;
    }

    public void fight(Meeple _target)
    {
            if (_target.GetType().IsSubclassOf(typeof (Companion)) || _target.GetType() == typeof (Companion))
            {
                int rolledValue = SceneHandler.rollDice(diceValue);
                Companion targetComp = (Companion) _target;

                Debug.Log(meepleName + " strikes " + _target.meepleName + " with a " + rolledValue);

                targetComp.damaged(rolledValue);
            }
            else
            {
                Debug.Log(meepleName + " strikes " + _target.meepleName);
                _target.Alive = false;
            }
        

        //Todo: Implememnt actionOutstanding if neccessary. As example when queded intelligent interaction is needed from opponent side.
        //hasActionOutstanding = false;
    }

        public void damaged(int _amount){
        //If companion already on 0 aka "bewusstlos". --> Kill companion.

            Strength -= _amount;

            if (strength <= 0 && _amount > 0)
            {
                Alive = false;
            }
        }

    public int Strength
    {
        get { return strength; }
        set
        {
            strength = Math.Max(value, 0);
            strength = Math.Min(value, strengthMax);
        }
    }
}
