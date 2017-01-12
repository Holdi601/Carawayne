using UnityEngine;
using System.Collections;

public class Scout : Companion {


    void Awake()
    {
        proviantDemand = 2;
        proviantDemandMax = 2;
        strength = 6;
        strengthMax = 10;
        walkRange = 4;
    }

    //public Scout(HexaPos _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    //{
    //}

    //public void scout(Tile[] _tile)
    //{
    //    hasActionOutstanding = false;
    //    //Todo: Missing implementation
    //}

    //public override void init(HexaPos _pos, string _meepleName)
    //{
    //    base.init(_pos, _meepleName);
    //}
}
