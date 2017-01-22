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
        setFoodPackages_hpBar();
    }

 


    //public override void init(HexaPos _pos, string _meepleName)
    //{
    //    base.init(_pos, _meepleName);
    //}
}
