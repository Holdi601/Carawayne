using UnityEngine;

public class HuntedAnimal : Meeple
{

    public int food;
    public bool hasEscaped;

    public HuntedAnimal(Vector2 _pos, string _meepleName, int _food) : base(_pos, _meepleName)
    {
        food = _food;
    }
}
