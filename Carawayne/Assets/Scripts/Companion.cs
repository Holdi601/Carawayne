﻿using System;
using UnityEngine;
using System.Collections;

public class Companion : Meeple {

    private int proviantDemand;
    private int proviantDemandMax;
    private int strength;
    private int strengthMax;
    private float proviantRation;
    private bool actionDone;

    public Companion(int _proviantDemand, int _strength)
    {
        proviantDemand = _proviantDemand;
        strength = _strength;
        strengthMax = _strength;
        proviantDemandMax = _proviantDemand;
        ProviantRation = 1.0f;
        actionDone = false;
    }

    //Consume a proviant ration
    public int consumeRation()
    {
        //Loose strength depending on difference between ProviantDemand and ProviantDemandMax
        Strength -= (proviantDemandMax - proviantDemand);

        //Return consumed proviant
        return proviantDemand;
    }

    //GETTER & SETTER
    //---------------
    public int Strength
    {
        get { return strength; }
        set {

            //If companion already on 0 aka "bewusstlos". --> Kill companion.
            if (strength <= 0 && value > 0)
            {
                //Todo: Trigger Death
                alive = false;
            }

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
}
