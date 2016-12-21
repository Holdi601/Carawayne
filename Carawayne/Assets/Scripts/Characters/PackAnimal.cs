using System;
using UnityEngine;

public class PackAnimal : Meeple
{

    private int actualLoad;
    private int loadCapacity;
    private int butcherAmount;

    //Todo: change Pos to struct
    public PackAnimal(Vector2 _pos, string _meepleName, int _loadCap, int _butcherAmount) : base(_pos, _meepleName)
    {
        actualLoad = _loadCap;
        loadCapacity = _loadCap;
        butcherAmount = _butcherAmount;
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
