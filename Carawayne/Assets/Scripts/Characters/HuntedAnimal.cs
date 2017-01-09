using UnityEngine;

public class HuntedAnimal : Meeple
{

    public int food;
    public bool hasEscaped;

    void Awake()
    {
        food = 4;
    }

    //public HuntedAnimal(HexaPos _pos, string _meepleName, int _food) : base(_pos, _meepleName)
    //{
    //    food = _food;
    //}

}
