using System;

public class PackAnimal : Meeple
{

    private int loadCap;
    private int loadCapMax;

    public PackAnimal(int _loadCap)
    {
        loadCap = _loadCap;
        loadCapMax = _loadCap;
    }

    //Loads an amount of proviant and returns overload value
    public int load(int _amount)
    {
        int rest = Math.Max((loadCap + _amount) - loadCapMax, 0);
        loadCap += _amount;
        return rest;
    }

    //Unloads an amount of proviant and returns overload value
    public int unload(int _amount) { 
            
        int rest = Math.Min((loadCap - _amount), 0);
        loadCap -= _amount;
        return rest;
    }

    //GETTER & SETTER
    //---------------
    public int LoadCap
    {
        get { return loadCap; }
        set
        {
            loadCap = Math.Max(value, 0);
            loadCap = Math.Min(value, loadCapMax);
        }
    }
}
