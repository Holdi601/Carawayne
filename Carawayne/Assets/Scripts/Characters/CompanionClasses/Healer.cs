using UnityEngine;

public class Healer: Companion
{

    public int healPower;

    //public Healer(HexaPos _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    //{
    //    healPower = 2;
    //}

    void Awake()
    {
        proviantDemand = 1;
        proviantDemandMax = 1;
        strength = 2;
        strengthMax = 10;
        healPower = 2;
        HasActionOutstanding = true;
    }

    public void heal(Companion _target)
    {
        Debug.Log(meepleName + " healed " + _target.meepleName + " with 2");
        _target.Strength += 2;
        HasActionOutstanding = false;
    }

    //public override void init(HexaPos _pos, string _meepleName)
    //{
    //    base.init(_pos, _meepleName);
    //    healPower = 2;
    //}
}
