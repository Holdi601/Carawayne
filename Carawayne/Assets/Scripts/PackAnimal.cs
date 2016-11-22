using System;
using UnityEngine;
using System.Collections;

public class PackAnimal : Meeple
{
    public int maxLoad;
    public int actualLoad;
    public int butcherFoodAmount;

    public int butcherAnimal()
    {
        gameObject.transform.parent = GameObject.Find("Graveyard").transform;
        return butcherFoodAmount;
    }

    public int load(int _amount)
    {
        int rest = Math.Max((ActualLoad + _amount) - maxLoad, 0);
        ActualLoad += _amount;
        return rest;
    }

    public int unload(int _amount)
    {
        int rest = Math.Min((ActualLoad - _amount), 0);
        ActualLoad -= _amount;
        return rest;
    }

    //GETTER SETTER
    //------------
    public int ActualLoad
    {
        get { return actualLoad; }
        set {
            actualLoad = value;

            if (actualLoad < 0)
                actualLoad = 0;
            else if (actualLoad > maxLoad)
                actualLoad = maxLoad;

            EventManager.TriggerEvent("FoodStockChanged");
        }
    }

    public int MaxLoad
    {
        get { return maxLoad; }
        set { maxLoad = value; }
    }
}
