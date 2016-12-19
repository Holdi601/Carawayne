using UnityEngine;

public class Healer: Companion
{

    public int healPower;

    public Healer(Vector2 _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    {
        healPower = 2;
    }

    public void heal(Companion _target)
    {
        Debug.Log(meepleName + " healed " + _target.meepleName + " with 2");
        _target.Strength += 2;
    }

}
